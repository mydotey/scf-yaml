using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using NLog;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace MyDotey.SCF.Yaml
{
    /**
     * @author koqizhao
     *
     * Jul 24, 2018
     * 
     * for Dictionaryping YAML File
     */
    public class YamlFileConfigurationSource : AbstractConfigurationSource<YamlFileConfigurationSourceConfig>
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger(typeof(YamlFileConfigurationSource));

        private YamlMappingNode _properties;

        public YamlFileConfigurationSource(YamlFileConfigurationSourceConfig config)
            : base(config)
        {
            try
            {
                using (Stream @is = new FileStream(config.FileName, FileMode.Open))
                {
                    if (@is == null)
                    {
                        Logger.Warn("file not found: {0}", config.FileName);
                        return;
                    }

                    StreamReader fileReader = new StreamReader(config.FileName, new UTF8Encoding(false));
                    YamlStream yamlStream = new YamlStream();
                    yamlStream.Load(fileReader);
                    if (!(yamlStream.Documents[0].RootNode is YamlMappingNode))
                    {
                        Logger.Error(typeof(YamlFileConfigurationSource).Name
                                + " only accepts YAML file with Mapping root. Current is Sequence or Scala. YAML file: "
                            + config.FileName);
                        return;
                    }
                    _properties = (YamlMappingNode)yamlStream.Documents[0].RootNode;
                }
            }
            catch (Exception e)
            {
                Logger.Warn(e, "failed to load yaml file: " + config.FileName);
            }
        }

        protected override object GetPropertyValue(object key)
        {
            if (_properties == null)
                return null;

            if (!(key is string))
                return null;

            _properties.Children.TryGetValue(new YamlScalarNode((string)key), out YamlNode value);
            return value;
        }

        protected override V Convert<K, V>(PropertyConfig<K, V> propertyConfig, object value)
        {
            V v = base.Convert(propertyConfig, value);
            if (!object.Equals(v, default(V)))
                return v;

            YamlNode node = (YamlNode)value;
            if (node.NodeType == YamlNodeType.Alias)
                return default(V);

            var deserializer = new DeserializerBuilder().WithNamingConvention(new CamelCaseNamingConvention()).Build();

            if (node.NodeType == YamlNodeType.Scalar)
                return deserializer.Deserialize<V>(((YamlScalarNode)node).Value);

            YamlDocument yamlDocument = new YamlDocument(node);
            YamlStream yamlStream = new YamlStream();
            yamlStream.Add(yamlDocument);
            using (MemoryStream stream = new MemoryStream())
            {
                StreamWriter writer = new StreamWriter(stream);
                yamlStream.Save(writer);
                writer.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                StreamReader reader = new StreamReader(stream);
                return deserializer.Deserialize<V>(reader);
            }
        }
    }
}