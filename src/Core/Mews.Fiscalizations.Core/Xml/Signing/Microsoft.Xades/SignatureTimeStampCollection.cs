using System.Collections;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// Collection class that derives from ArrayList.  It provides the minimally
/// required functionality to add instances of typed classes and obtain typed
/// elements through a custom indexer.
/// </summary>
internal sealed class SignatureTimeStampCollection : ArrayList
{
	/// <summary>
	/// New typed indexer for the collection
	/// </summary>
	/// <param name="index">Index of the object to retrieve from collection</param>
	public new TimeStamp this[int index]
	{
		get => (TimeStamp)base[index];
		set => base[index] = value;
	}

	/// <summary>
	/// Add typed object to the collection
	/// </summary>
	/// <param name="objectToAdd">Typed object to be added to collection</param>
	/// <returns>The object that has been added to collection</returns>
	public TimeStamp Add(TimeStamp objectToAdd)
	{
		base.Add(objectToAdd);

		return objectToAdd;
	}

	/// <summary>
	/// Add new typed object to the collection
	/// </summary>
	/// <param name="tagName">Name of the tag when serializing into XML using GetXml()</param>
	/// <returns>The newly created object that has been added to collection</returns>
	public TimeStamp Add(string tagName)
	{
		return Add(new TimeStamp(tagName));
	}
}