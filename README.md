# CryptoService #

**CryptoService** provides one-way hash and two-way encryption and decryption services.


## Prerequisites ##

**CryptoService** requires the following to be installed on your box depending on your purpose of use:

- [Micrisoft .NET Framework 4.5](http://www.microsoft.com/en-us/download/details.aspx?id=30653)


## Download Builds ##

Download pre-built application and library are available at this [download page](http://aliencube.github.io/CryptoService/downloads.html).


## Getting Started &ndash; Console Application ##

In order to run **CryptoService** console app, a configuration file &ndash; `Aliencube.CryptoService.ConsoleApp.exe.config` &ndash; must be setup beforehand.


### Usage ###

    AlienCryptoService.exe (/e|/d) /m:"TEXT" [/f:FILENAME] [/o:FILENAME]


### Parameters ###

    /e             Encrypt the text or text file.
    /d             Decrypt the text or text file.
    /t:"[TEXT]"    Text to encrypt or decrypt. It must be enclosed by double quotes.
    /f:[FILENAME]  Filename containing text to encrypt or decrypt.
    /o:[FILENAME]  Filename to store text encrypted or decrypted.

**NOTE: `/e` and `/d` cannot be used at the same time.**


## Getting Started &ndash; CryptoService Library ##

In order to use **CryptoService** in your applications, simply copy `Aliencube.CryptoService.dll` to the application's `/bin` directory or reference `Aliencube.CryptoService.dll` to your application project.


### Hash Service ###

.NET framework provides five different hash algorithms, `MD5`, `SHA1`, `SHA256`, `SHA384` and `SHA512`. **CryptoService** supports all those hash algorithms.

```c#
var provider = HashProvider.SHA256           // Sets the hash algorithm to SHA256
var hashService = new HashService(provider); // Creates a new HashService instance

var input = "Hello World";                   // Sets the input text to "Hello World"
var output = hashService.Hash(input);        // Gets the hashed value to output

Console.WriteLine("Input: {0}", input);
Console.WriteLine("Output: {0}", output);

// Input: Hello World
// Output: pZGm1Av0IEBKARczz7exkNYsZb8LzaMrV7J32a2fFG4=
```

Please note that **CryptoService** does not put salt key for hashing. So it is your own responsibility to add any salt key before hashing.


### Symmetric Service ###

.NET framework provides five different symmetric algorithms, `Aes`, `DES`, `RC2`, `Rijndael` and `TripleDES`. **CryptoService** supports all those symmetric algorithms.

```c#
var provider = SymmetricProvider.Aes                   // Sets the symmetric algorithm to Aes
var symmetricService = new SymmetricService(provider); // Creates a new SymmetricService instance
symmetricService.Key = "KEY_VALUE";                    // Sets the key value.
symmetricService.Vector = "VECTOR_VALUE";              // Sets the vector value.

var input = "Hello World";                             // Sets the input text to "Hello World"
var encrypted = symmetricService.Encrypt(input);       // Gets the encrypted value to encrypted
var decrypted = symmetricService.Decrypt(encrypted);   // Gets the decrypted value to decrypted

Console.WriteLine("Input: {0}", input);
Console.WriteLine("Encrypted: {0}", encrypted);
Console.WriteLine("Decrypted: {0}", decrypted);

// Input: Hello World
// Encrypted: bdwO/5C8a+pliIoIXtuzfA==
// Decrypted: Hello World
```

Depending on the symmetric algorithm chosen, the length of key and vector are different.

* `Aes`: 32/16
* `DES`: 8/8
* `RC2`: 16/8
* `Rijndael`: 32/16
* `TripleDES`: 24/8

Please note that **CryptoService** does not put salt key for encryption. So it is your own responsibility to add any salt key before encryption.


## License ##

**CryptoService** is released under the [MIT License](http://opensource.org/licenses/MIT).

> The MIT License (MIT)
> Copyright (c) 2013 [aliencube.org](http://aliencube.org)
> 
> Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
> 
> The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
> 
> THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

