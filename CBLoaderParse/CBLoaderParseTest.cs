using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using CharacterBuilderBrowser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CBLoaderParse
{
	[TestClass]
	public class CBLoaderParseTest
	{
		[TestMethod]
		public void DoCBLoaderParse()
		{
			D20Rules rules;
			var xs=new XmlSerializer(typeof(D20Rules));
			using(var xmlReader=XmlReader.Create(new StreamReader(File.OpenRead("C:\\Users\\Shaangor\\AppData\\Roaming\\CBLoader\\combined.dnd40"))))
			{
				rules=(D20Rules)xs.Deserialize(xmlReader);
			}
			Assert.IsNotNull(rules);
			var elements=rules.RulesElements.Where(e => e.Name.Contains("Eldritch")).ToList();
			Assert.IsTrue(elements.Count>0);
		}
	}
}
