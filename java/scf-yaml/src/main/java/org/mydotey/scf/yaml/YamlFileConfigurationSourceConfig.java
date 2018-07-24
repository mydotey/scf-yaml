package org.mydotey.scf.yaml;

import org.mydotey.scf.DefaultConfigurationSourceConfig;

/**
 * @author koqizhao
 *
 * May 17, 2018
 */
public class YamlFileConfigurationSourceConfig extends DefaultConfigurationSourceConfig {

    private String _fileName;

    protected YamlFileConfigurationSourceConfig() {

    }

    public String getFileName() {
        return _fileName;
    }

    @Override
    public String toString() {
        return String.format("%s { name: %s, fileName: %s }", getClass().getSimpleName(), getName(), getFileName());
    }

    public static class Builder extends DefaultConfigurationSourceConfig.DefaultAbstractBuilder<Builder> {

        @Override
        protected YamlFileConfigurationSourceConfig newConfig() {
            return new YamlFileConfigurationSourceConfig();
        }

        @Override
        protected YamlFileConfigurationSourceConfig getConfig() {
            return (YamlFileConfigurationSourceConfig) super.getConfig();
        }

        /**
         * @param fileName a Mapping YAML file
         * @return the builder itself
         */
        public Builder setFileName(String fileName) {
            getConfig()._fileName = fileName;
            return this;
        }

        @Override
        public YamlFileConfigurationSourceConfig build() {
            if (getConfig().getFileName() == null || getConfig().getFileName().trim().isEmpty())
                throw new IllegalArgumentException("fileName is null or empty");

            getConfig()._fileName = getConfig()._fileName.trim();
            if (!getConfig()._fileName.endsWith(".yaml") && !getConfig()._fileName.endsWith(".yml"))
                getConfig()._fileName += ".yaml";

            return (YamlFileConfigurationSourceConfig) super.build();
        }

    }

}
