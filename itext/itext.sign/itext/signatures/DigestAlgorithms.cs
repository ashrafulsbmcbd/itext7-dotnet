/*
*
* This file is part of the iText (R) project.
Copyright (c) 1998-2017 iText Group NV
* Authors: Bruno Lowagie, Paulo Soares, et al.
*
* This program is free software; you can redistribute it and/or modify
* it under the terms of the GNU Affero General Public License version 3
* as published by the Free Software Foundation with the addition of the
* following permission added to Section 15 as permitted in Section 7(a):
* FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
* ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
* OF THIRD PARTY RIGHTS
*
* This program is distributed in the hope that it will be useful, but
* WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
* or FITNESS FOR A PARTICULAR PURPOSE.
* See the GNU Affero General Public License for more details.
* You should have received a copy of the GNU Affero General Public License
* along with this program; if not, see http://www.gnu.org/licenses or write to
* the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
* Boston, MA, 02110-1301 USA, or download the license from the following URL:
* http://itextpdf.com/terms-of-use/
*
* The interactive user interfaces in modified source and object code versions
* of this program must display Appropriate Legal Notices, as required under
* Section 5 of the GNU Affero General Public License.
*
* In accordance with Section 7(b) of the GNU Affero General Public License,
* a covered work must retain the producer line in every PDF that is created
* or manipulated using iText.
*
* You can be released from the requirements of the license by purchasing
* a commercial license. Buying such a license is mandatory as soon as you
* develop commercial activities involving the iText software without
* disclosing the source code of your own applications.
* These activities include: offering paid services to customers as an ASP,
* serving PDFs on the fly in a web application, shipping iText with a closed
* source product.
*
* For more information, please contact iText Software Corp. at this
* address: sales@itextpdf.com
*/
using System;
using System.Collections.Generic;
using System.IO;
using Org.BouncyCastle.Crypto;

namespace iText.Signatures {
    /// <summary>Class that contains a map with the different message digest algorithms.</summary>
    public class DigestAlgorithms {
        /// <summary>Algorithm available for signatures since PDF 1.3.</summary>
        public const String SHA1 = "SHA-1";

        /// <summary>Algorithm available for signatures since PDF 1.6.</summary>
        public const String SHA256 = "SHA-256";

        /// <summary>Algorithm available for signatures since PDF 1.7.</summary>
        public const String SHA384 = "SHA-384";

        /// <summary>Algorithm available for signatures since PDF 1.7.</summary>
        public const String SHA512 = "SHA-512";

        /// <summary>Algorithm available for signatures since PDF 1.7.</summary>
        public const String RIPEMD160 = "RIPEMD160";

        /// <summary>Maps the digest IDs with the human-readable name of the digest algorithm.</summary>
        private static readonly IDictionary<String, String> digestNames = new Dictionary<String, String>();

        /// <summary>Maps digest algorithm that are unknown by the JDKs MessageDigest object to a known one.</summary>
        private static readonly IDictionary<String, String> fixNames = new Dictionary<String, String>();

        /// <summary>Maps the name of a digest algorithm with its ID.</summary>
        private static readonly IDictionary<String, String> allowedDigests = new Dictionary<String, String>();

        static DigestAlgorithms() {
            digestNames["1.2.840.113549.2.5"] = "MD5";
            digestNames["1.2.840.113549.2.2"] = "MD2";
            digestNames["1.3.14.3.2.26"] = "SHA1";
            digestNames["2.16.840.1.101.3.4.2.4"] = "SHA224";
            digestNames["2.16.840.1.101.3.4.2.1"] = "SHA256";
            digestNames["2.16.840.1.101.3.4.2.2"] = "SHA384";
            digestNames["2.16.840.1.101.3.4.2.3"] = "SHA512";
            digestNames["1.3.36.3.2.2"] = "RIPEMD128";
            digestNames["1.3.36.3.2.1"] = "RIPEMD160";
            digestNames["1.3.36.3.2.3"] = "RIPEMD256";
            digestNames["1.2.840.113549.1.1.4"] = "MD5";
            digestNames["1.2.840.113549.1.1.2"] = "MD2";
            digestNames["1.2.840.113549.1.1.5"] = "SHA1";
            digestNames["1.2.840.113549.1.1.14"] = "SHA224";
            digestNames["1.2.840.113549.1.1.11"] = "SHA256";
            digestNames["1.2.840.113549.1.1.12"] = "SHA384";
            digestNames["1.2.840.113549.1.1.13"] = "SHA512";
            digestNames["1.2.840.113549.2.5"] = "MD5";
            digestNames["1.2.840.113549.2.2"] = "MD2";
            digestNames["1.2.840.10040.4.3"] = "SHA1";
            digestNames["2.16.840.1.101.3.4.3.1"] = "SHA224";
            digestNames["2.16.840.1.101.3.4.3.2"] = "SHA256";
            digestNames["2.16.840.1.101.3.4.3.3"] = "SHA384";
            digestNames["2.16.840.1.101.3.4.3.4"] = "SHA512";
            digestNames["1.3.36.3.3.1.3"] = "RIPEMD128";
            digestNames["1.3.36.3.3.1.2"] = "RIPEMD160";
            digestNames["1.3.36.3.3.1.4"] = "RIPEMD256";
            digestNames["1.2.643.2.2.9"] = "GOST3411";
            fixNames["SHA256"] = SHA256;
            fixNames["SHA384"] = SHA384;
            fixNames["SHA512"] = SHA512;
            allowedDigests["MD2"] = "1.2.840.113549.2.2";
            allowedDigests["MD-2"] = "1.2.840.113549.2.2";
            allowedDigests["MD5"] = "1.2.840.113549.2.5";
            allowedDigests["MD-5"] = "1.2.840.113549.2.5";
            allowedDigests["SHA1"] = "1.3.14.3.2.26";
            allowedDigests["SHA-1"] = "1.3.14.3.2.26";
            allowedDigests["SHA224"] = "2.16.840.1.101.3.4.2.4";
            allowedDigests["SHA-224"] = "2.16.840.1.101.3.4.2.4";
            allowedDigests["SHA256"] = "2.16.840.1.101.3.4.2.1";
            allowedDigests["SHA-256"] = "2.16.840.1.101.3.4.2.1";
            allowedDigests["SHA384"] = "2.16.840.1.101.3.4.2.2";
            allowedDigests["SHA-384"] = "2.16.840.1.101.3.4.2.2";
            allowedDigests["SHA512"] = "2.16.840.1.101.3.4.2.3";
            allowedDigests["SHA-512"] = "2.16.840.1.101.3.4.2.3";
            allowedDigests["RIPEMD128"] = "1.3.36.3.2.2";
            allowedDigests["RIPEMD-128"] = "1.3.36.3.2.2";
            allowedDigests["RIPEMD160"] = "1.3.36.3.2.1";
            allowedDigests["RIPEMD-160"] = "1.3.36.3.2.1";
            allowedDigests["RIPEMD256"] = "1.3.36.3.2.3";
            allowedDigests["RIPEMD-256"] = "1.3.36.3.2.3";
            allowedDigests["GOST3411"] = "1.2.643.2.2.9";
        }

        /// <summary>Get a digest algorithm.</summary>
        /// <param name="digestOid">oid of the digest algorithm</param>
        /// <param name="provider">the provider you want to use to create the hash</param>
        /// <returns>MessageDigest object</returns>
        /// <exception cref="Org.BouncyCastle.Security.SecurityUtilityException"/>
        /// <exception cref="Java.Security.NoSuchProviderException"/>
        public static IDigest GetMessageDigestFromOid(String digestOid) {
            return GetMessageDigest(GetDigest(digestOid));
        }

        /// <summary>Creates a MessageDigest object that can be used to create a hash.</summary>
        /// <param name="hashAlgorithm">the algorithm you want to use to create a hash</param>
        /// <param name="provider">the provider you want to use to create the hash</param>
        /// <returns>a MessageDigest object</returns>
        /// <exception cref="Org.BouncyCastle.Security.SecurityUtilityException"/>
        /// <exception cref="Java.Security.NoSuchProviderException"/>
        public static IDigest GetMessageDigest(String hashAlgorithm) {
            return SignUtils.GetMessageDigest(hashAlgorithm);
        }

        /// <summary>Creates a hash using a specific digest algorithm and a provider.</summary>
        /// <param name="data">the message of which you want to create a hash</param>
        /// <param name="hashAlgorithm">the algorithm used to create the hash</param>
        /// <param name="provider">the provider used to create the hash</param>
        /// <returns>the hash</returns>
        /// <exception cref="Org.BouncyCastle.Security.GeneralSecurityException"/>
        /// <exception cref="System.IO.IOException"/>
        public static byte[] Digest(Stream data, String hashAlgorithm) {
            IDigest messageDigest = GetMessageDigest(hashAlgorithm);
            return Digest(data, messageDigest);
        }

        /// <summary>Create a digest based on the inputstream.</summary>
        /// <param name="data">data to be digested</param>
        /// <param name="messageDigest">algorithm to be used</param>
        /// <returns>digest of the data</returns>
        /// <exception cref="Org.BouncyCastle.Security.GeneralSecurityException"/>
        /// <exception cref="System.IO.IOException"/>
        public static byte[] Digest(Stream data, IDigest messageDigest) {
            byte[] buf = new byte[8192];
            int n;
            while ((n = data.Read(buf)) > 0) {
                messageDigest.Update(buf, 0, n);
            }
            return messageDigest.Digest();
        }

        /// <summary>Gets the digest name for a certain id</summary>
        /// <param name="oid">an id (for instance "1.2.840.113549.2.5")</param>
        /// <returns>a digest name (for instance "MD5")</returns>
        public static String GetDigest(String oid) {
            String ret = digestNames.Get(oid);
            if (ret == null) {
                return oid;
            }
            else {
                return ret;
            }
        }

        /// <summary>
        /// Returns the id of a digest algorithms that is allowed in PDF,
        /// or null if it isn't allowed.
        /// </summary>
        /// <param name="name">The name of the digest algorithm.</param>
        /// <returns>An oid.</returns>
        public static String GetAllowedDigest(String name) {
            return allowedDigests.Get(name.ToUpperInvariant());
        }
    }
}
