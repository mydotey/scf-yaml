package org.mydotey.scf.yaml;

import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;

/**
 * @author koqizhao
 *
 * Jul 24, 2018
 * 
 * for Mapping YAML File
 */
public class LocalYamlFileConfigurationSource extends YamlFileConfigurationSource {

    public LocalYamlFileConfigurationSource(YamlFileConfigurationSourceConfig config) {
        super(config);
    }

    @Override
    protected InputStream loadFile(String fileName) throws IOException {
        return new FileInputStream(fileName);
    }

}
