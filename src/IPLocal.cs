/* 
The MIT License (MIT)

Copyright (c) 2017 Wolfgang Almeida <wolfgang.almeida@yahoo.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Linq;

namespace IPSharpNS
{
    public class IPLocal
    {
        // Default Constructor
        // ===================
        public IPLocal() { }

        // Member Methods
        // ==============

        // Public Methods:
        // ---------------

        // Displaying results for the user
        // -------------------------------
        public void DisplayResults()
        {
            GetInterfaceInformation();
        }

        // Private Methods:
        // ---------------

        // Getting some information from all available Ethernet and Wireless interfaces
        // ----------------------------------------------------------------------------
        private void GetInterfaceInformation()
        {
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface intf in interfaces)
            {
                if (intf.NetworkInterfaceType == NetworkInterfaceType.Ethernet || intf.NetworkInterfaceType == NetworkInterfaceType.Wireless80211) {
                    Console.WriteLine("{0}", intf.Description);
                    Console.WriteLine(string.Concat(Enumerable.Repeat("-", intf.Description.Length)));
                    Console.WriteLine("Tipo de Interface: {0}", intf.NetworkInterfaceType.ToString());

                    foreach (UnicastIPAddressInformation ip in intf.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork) {
                            Console.WriteLine("Endereço IP (IPv4): {0}", ip.Address.ToString());
                            Console.WriteLine("Máscara de Sub-Rede: {0}", ip.IPv4Mask.ToString());
                            if (intf.GetIPProperties().GatewayAddresses.Any() == true) {
                                Console.WriteLine("Gateway Padrão: {0}\n", intf.GetIPProperties().GatewayAddresses.FirstOrDefault().Address);
                            }
                            else {
                                Console.WriteLine("Gateway Padrão: {0}\n", "");
                            }
                        }
                        else if (ip.Address.AddressFamily == AddressFamily.InterNetworkV6) {
                            Console.WriteLine("Endereço IP (IPv6): {0}", ip.Address.ToString());
                        }
                    }
                }
            }
        }
    }
}
