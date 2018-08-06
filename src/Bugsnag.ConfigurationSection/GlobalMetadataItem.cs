using System.Configuration;

namespace Bugsnag.ConfigurationSection
{
  [ConfigurationCollection(typeof(GlobalMetadataItem), AddItemName = "item", CollectionType = ConfigurationElementCollectionType.BasicMap)]
  class GlobalMetadataCollection : ConfigurationElementCollection
  {
    protected override ConfigurationElement CreateNewElement() =>
      new GlobalMetadataItem();

    protected override object GetElementKey(ConfigurationElement element) =>
      ((GlobalMetadataItem)element).Key;
  }

  class GlobalMetadataItem : ConfigurationElement
  {
    [ConfigurationProperty("key", IsRequired = true)]
    public string Key => (string)this["key"];

    [ConfigurationProperty("value", IsRequired = true)]
    public string Value => (string)this["value"];
  }
}
