package org.mydotey.scf.yaml;

import java.io.IOException;
import java.io.InputStream;
import java.util.HashMap;
import java.util.Map;

import org.mydotey.scf.AbstractConfigurationSource;
import org.mydotey.scf.PropertyConfig;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.yaml.snakeyaml.Yaml;

import com.fasterxml.jackson.databind.ObjectMapper;

/**
 * @author koqizhao
 *
 * Jul 24, 2018
 * 
 * for Mapping YAML File
 */
public class YamlFileConfigurationSource extends AbstractConfigurationSource<YamlFileConfigurationSourceConfig> {

    private static final Logger LOGGER = LoggerFactory.getLogger(YamlFileConfigurationSource.class);

    private Map<Object, Object> _properties;
    private ObjectMapper _objectMapper;

    public YamlFileConfigurationSource(YamlFileConfigurationSourceConfig config) {
        super(config);

        _objectMapper = newObjectMapper();

        init();
    }

    @SuppressWarnings("unchecked")
    protected void init() {
        Object properties = loadYamlProperties();
        if (checkSupported(properties)) {
            _properties = (Map<Object, Object>) properties;
            return;
        }

        _properties = new HashMap<>();
    }

    protected Object loadYamlProperties() {
        try (InputStream is = loadFile(getConfig().getFileName())) {
            if (is == null) {
                LOGGER.warn("file not found: {}", getConfig().getFileName());
                return null;
            }

            return new Yaml().load(is);
        } catch (Exception e) {
            LOGGER.warn("failed to load yaml file: " + getConfig().getFileName(), e);
            return null;
        }
    }

    protected InputStream loadFile(String fileName) throws IOException {
        return Thread.currentThread().getContextClassLoader().getResourceAsStream(fileName);
    }

    protected boolean checkSupported(Object yamlData) {
        if (yamlData == null)
            return false;

        if (yamlData instanceof Map)
            return true;

        LOGGER.error(this.getClass().getSimpleName()
                + " only accepts YAML file with Mapping root. Current is Sequence or Scala. YAML file: "
                + getConfig().getFileName());
        return false;
    }

    protected void updateProperties(Map<Object, Object> properties) {
        _properties = properties == null ? new HashMap<>() : properties;
        raiseChangeEvent();
    }

    protected ObjectMapper newObjectMapper() {
        return new ObjectMapper();
    }

    @Override
    protected Object getPropertyValue(Object key) {
        return _properties.get(key);
    }

    @Override
    protected <K, V> V convert(PropertyConfig<K, V> propertyConfig, Object value) {
        V v = super.convert(propertyConfig, value);
        if (v != null)
            return v;

        return _objectMapper.convertValue(value, propertyConfig.getValueType());
    }

}
