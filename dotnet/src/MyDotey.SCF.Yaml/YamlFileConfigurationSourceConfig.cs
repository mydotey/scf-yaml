using System;

namespace MyDotey.SCF.Yaml
{
    /**
     * @author koqizhao
     *
     * May 17, 2018
     */
    public class YamlFileConfigurationSourceConfig : DefaultConfigurationSourceConfig
    {
        private string _fileName;

        protected YamlFileConfigurationSourceConfig()
        {

        }

        public virtual string FileName { get { return _fileName; } }

        public override string ToString()
        {
            return string.Format("{0} {{ name: {1}, fileName: {2} }}", GetType().Name, Name, FileName);
        }

        public new class Builder :
                DefaultConfigurationSourceConfig.DefaultAbstractBuilder<Builder, YamlFileConfigurationSourceConfig>
        {
            protected override YamlFileConfigurationSourceConfig NewConfig()
            {
                return new YamlFileConfigurationSourceConfig();
            }

            public virtual Builder SetFileName(string fileName)
            {
                Config._fileName = fileName;
                return this;
            }

            public override YamlFileConfigurationSourceConfig Build()
            {
                if (string.IsNullOrWhiteSpace(Config.FileName))
                    throw new ArgumentNullException("fileName is null or empty");

                Config._fileName = Config._fileName.Trim();
                if (!Config._fileName.EndsWith(".yaml") && !Config._fileName.EndsWith(".yml"))
                    Config._fileName += ".yaml";

                return base.Build();
            }
        }
    }
}