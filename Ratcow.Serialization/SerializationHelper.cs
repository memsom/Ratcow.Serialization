﻿/*
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Ratcow.Serialization
{
    /// <summary>
    /// This is a simple generic wrapper to aid serializing and deserializing to 
    /// XML. This is boiler plate code that I keep writing and putting it here
    /// means I hopefully don't need to write it again.
    /// 
    /// This doesn't do anything clever at the moment, but could be extended in
    /// the future to inject extra namespaces etc.
    /// </summary>
    public static class SerializationHelper
    {
        public static string SerializeDefaultEncoding<T>(T instance)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stringWriter = new StringWriter())
            {
                serializer.Serialize(stringWriter, instance);
                return stringWriter.ToString();
            }
        }

        public static string Serialize<T>(T instance)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stringWriter = new Utf8StringWriter())
            {
                serializer.Serialize(stringWriter, instance);
                return stringWriter.ToString();
            }
        }

        public static object Deserialize(Type t, string data)
        {
            var serializer = new XmlSerializer(t);
            using (var stringReader = new StringReader(data))
            {
                return serializer.Deserialize(stringReader);
            }
        }

        public static T Deserialize<T>(string data)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stringReader = new StringReader(data))
            {
                return (T)serializer.Deserialize(stringReader);
            }
        }
    }
}
