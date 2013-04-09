# AlienCryptoService #

This library provides one-way hash and two-way encryption and decryption services.


## Prerequisites ##

**AlienCryptoService** requires the following to be installed on your box depending on your purpose of use:

- [Micrisoft .NET Framework 4.5](http://www.microsoft.com/en-us/download/details.aspx?id=30653)


## Download Builds ##

Download pre-built application and library are available at [BitBucket](https://bitbucket.org/aliencube/cryptoservice/downloads).


## Getting Started ##

- In order to run **AlienCryptoService** console app, a configuration file - `AlienCryptoService.exe.config` - needs to be setup beforehand.
- In order to use **AlienCryptoService** in other applications, simply copy `Aliencube.CryptoService.dll` to the application's `/bin` directory or reference `Aliencube.CryptoService.dll` to the application project.


## Usage ##

    AlienCryptoService.exe (/e|/d) /m:"[TEXT]" [/f:[FILENAME]] [/o:[FILENAME]]


## Parameters ##

    /e             Encrypt the text or text file.
    /d             Decrypt the text or text file.
    /t:"[TEXT]"    Text to encrypt or decrypt. It must be enclosed by double quotes.
    /f:[FILENAME]  Filename containing text to encrypt or decrypt.
    /o:[FILENAME]  Filename to store text encrypted or decrypted.

**NOTE: `/e` and `/d` cannot be used at the same time.**


## Change History ##

### 0.7.0.0 ###

- Initial release


## License ##

**AlienCryptoService** is released under the [MIT License](http://opensource.org/licenses/MIT).

> The MIT License (MIT)
> Copyright (c) 2013 [aliencube.org](http://aliencube.org)
> 
> Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
> 
> The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
> 
> THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

