using System.Xml.Serialization;

namespace Mews.Fiscalizations.Spain.Dto.Responses
{
	[XmlRoot(ElementName = "detail")]
	public class Detail
	{
		[XmlElement(ElementName = "callstack")]
		public string Callstack { get; set; }
	}

	[XmlRoot(ElementName = "Fault")]
	public class Fault
	{
		[XmlElement(ElementName = "faultcode")]
		public string Faultcode { get; set; }
		[XmlElement(ElementName = "faultstring")]
		public string Faultstring { get; set; }
		[XmlElement(ElementName = "detail")]
		public Detail Detail { get; set; }
	}

	[XmlRoot(ElementName = "Body")]
	public class Body
	{
		[XmlElement(ElementName = "Fault")]
		public Fault Fault { get; set; }
	}

	[XmlRoot(ElementName = "Envelope")]
	public class SoapFaultResponse
	{
		[XmlElement(ElementName = "Body")]
		public Body Body { get; set; }
		[XmlAttribute(AttributeName = "env")]
		public string Env { get; set; }
		[XmlText]
		public string Text { get; set; }
	}
}