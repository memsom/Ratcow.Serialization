/*
 * Copyright 2018 Rat Cow Software and Matt Emson. All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, are
 * permitted provided that the following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice, this list of
 *    conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright notice, this list
 *    of conditions and the following disclaimer in the documentation and/or other materials
 *    provided with the distribution.
 * 3. Neither the name of the Rat Cow Software nor the names of its contributors may be used
 *    to endorse or promote products derived from this software without specific prior written
 *    permission.
 *
 * THIS SOFTWARE IS PROVIDED BY RAT COW SOFTWARE "AS IS" AND ANY EXPRESS OR IMPLIED
 * WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
 * FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
 * ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
 * ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *
 * The views and conclusions contained in the software and documentation are those of the
 * authors and should not be interpreted as representing official policies, either expressed
 * or implied, of Rat Cow Software and Matt Emson.
 *
 */

using System.Xml;

namespace Ratcow.Serialization
{
    /// <summary>
    /// This class pulls together a bunch of tools used to maipulate
    /// bare Xml to create an XmlElement. This is expecially useful when 
    /// trying to construct an instance of a class based on an XSD spec 
    /// where .Net decided to drop to XmlElement as the type.
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        /// This is a means to an end
        /// </summary>
        public static string GetRootElementName(string xml)
        {
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(xml);
                return doc.DocumentElement.Name;
            }
            catch (XmlException)
            {
                return null;
            }            
        }

        /// <summary>
        /// Serializes an instance in to an XmlNode
        /// </summary>
        public static XmlElement InstanceToElement<T>(T instance)
        {
            var instanceString = SerializationHelper.Serialize(instance);
            return StringToElement(instanceString);
        }

        /// <summary>
        /// Creates an XmlNode from an arbitrary Xml block
        /// </summary>
        public static XmlElement StringToElement(string data)
        {
            var doc = new XmlDocument();
            var fragment = doc.CreateDocumentFragment();

            fragment.InnerXml = data;

            doc.AppendChild(fragment);

            return doc.DocumentElement;
        }
    }
}
