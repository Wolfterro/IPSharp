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

namespace IPSharpNS
{
    public class GlobalVars
    {
        public static string Version = "1.1";
    }

    class Program
    {
        static void DisplayInternalIP()
        {
            IPLocal ip = new IPLocal();
            ip.DisplayResults();
        }

        static void DisplayExternalIP(string SelectedIP)
        {
            IPSharp ipsharp = new IPSharp(SelectedIP);
            ipsharp.DisplayResults();
        }

        static void HelpMenu()
        {
            Console.WriteLine("====================");
            Console.WriteLine("IPSharp - Versão {0}", (GlobalVars.Version));
            Console.WriteLine("====================\n");

            Console.WriteLine("Uso: ./IPSharp.exe [OPÇÕES] [IP]");
            Console.WriteLine("--------------------------------\n");

            Console.WriteLine("[Opções]");
            Console.WriteLine("--------");
            Console.WriteLine(" -h || --help\t\tMostra este menu de ajuda.");
            Console.WriteLine(" -i || --ip\t\tMostra as informações do endereço IP selecionado.");
            Console.WriteLine(" -l || --local\t\tMostra as informações das interfaces de rede da máquina.\n");

            Console.WriteLine("Nota:");
            Console.WriteLine("-----\n");
            Console.WriteLine("Este programa faz uso da API do IPInfo para dar informações sobre o endereço IP.");
            Console.WriteLine("Este programa faz uso da API do Google Maps para decodificar a localização estimada dos IP's.\n");

            Console.WriteLine("Porém, há um limite diário de 1000 verificações para o IPInfo, caso exceda este limite, você");
            Console.WriteLine("poderá ficar sem a informação sobre os endereços que você escolher! Caso isto ocorra, deverá");
            Console.WriteLine("esperar um período de 24 horas até a próxima consulta!\n");

            Console.WriteLine("Para verificações das interfaces de rede locais da máquina, o programa não faz uso de nenhuma");
            Console.WriteLine("API, portanto não requer conexão com a internet ou possui qualquer limite de verificação!\n");

            Console.WriteLine("----------------------------------------------------------------------------------\n");

            Console.WriteLine(" *** Este programa é licenciado sob a licença MIT ***\n");
            Console.WriteLine("Copyright (c) 2017 Wolfgang Almeida <wolfgang.almeida@yahoo.com>");
            Console.WriteLine("Repositório no GitHub: {0}\n", "https://github.com/Wolfterro/IPSharp");

            Environment.Exit(0);
        }

        static void Main(string[] args)
        {
            if (args.Length >= 2) {
                if (args[0].Contains("-i") || args[0].Contains("--ip")) {
                    DisplayExternalIP(string.Format("{0}", args[1]));
                }
                if (args[0].Contains("-h") || args[0].Contains("--help")) {
                    HelpMenu();
                }
                if (args[0].Contains("-l") || args[0].Contains("--local")) {
                    DisplayInternalIP();
                }
            }
            else if (args.Length == 1) {
                if (args[0].Contains("-i") || args[0].Contains("--ip")) {
                    Console.WriteLine("[IPSharp] Erro! Nenhum endereço IP inserido! Saindo...");
                    Environment.Exit(1);
                }
                if (args[0].Contains("-h") || args[0].Contains("--help")) {
                    HelpMenu();
                }
                if (args[0].Contains("-l") || args[0].Contains("--local"))
                {
                    DisplayInternalIP();
                }
            }
            else {
                DisplayExternalIP(null);
            }
        }
    }
}
