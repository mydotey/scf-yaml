package org.mydotey.scf.yaml;

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
public class YamlFileConfigurationSource extends AbstractConfigurationSource {

    private static final Logger LOGGER = LoggerFactory.getLogger(YamlFileConfigurationSource.class);

    private Map<Object, Object> _properties;
    private ObjectMapper _objectMapper;

    public YamlFileConfigurationSource(YamlFileConfigurationSourceConfig config) {
        super(config);

        try (InputStream is = Thread.currentThread().getContextClassLoader()
                .getResourceAsStream(config.getFileName())) {
            if (is == null) {
                _properties = new HashMap<>();
                LOGGER.warn("file not found: {}", config.getFileName());
                return;
            }

            Object properties = new Yaml().load(is);
            if (properties == null || !(properties instanceof Map)) {
                _properties = new HashMap<>();
                LOGGER.error(YamlFileConfigurationSource.class.getSimpleName()
                        + " only accepts YAML file with Mapping root. Current is Sequence or Scala. YAML file: "
                        + config.getFileName());
                return;
            }

            _objectMapper = new ObjectMapper();
        } catch (Exception e) {
            LOGGER.warn("failed to load yaml file: " + config.getFileName(), e);
        }
    }

    @Override
    protected Object getPropertyValue(Object key) {
        if (_properties == null)
            return null;

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
