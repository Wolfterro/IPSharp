# IPSharp
## Obtenha detalhes sobre seu endereço IP utilizando as API's da IPInfo e Google Maps!

### Descrição:

##### Este programa possui a função de mostrar e verificar as informações do seu endereço IP ou de qualquer outro endereço IP que for inserido.
##### Ao executar o programa sem usar opções, ele irá mostrar as informações do endereço IP do usuário: o seu endereço IP externo, hostname, cidade, região, país, código postal, endereço (estimado), latitude/longitude e organização.
##### É possível também inserir um endereço IP qualquer para que o programa possa verificar suas informações e mostrar ao usuário.

### **Aviso**:

##### A API que o programa utiliza para verificar os endereços IP é fornecida pela [IPInfo](https://ipinfo.io), portanto possui um **limite diário de 1000 verificações**!
##### Caso exceda este limite, você deverá esperar 24 horas até que possa fazer novas verificações!

### Uso:
##### Aqui estão os possíveis argumentos que poderão ser inseridos na hora de executar o programa:

    Uso: ./IPSharp.exe [OPÇÕES] [IP]
    --------------------------------
    
    Opções:
    -------
    '-h' ou '--help':  Mostra o menu de ajuda.
    '-i' ou '--ip':    Mostra as informações do endereço IP selecionado.

### Requisitos:
- ***Windows:*** Microsoft .NET Framework 4.0 ou mais recente
- ***Linux/Outros:*** Mono 4.0 ou mais recente

##### A biblioteca Json.NET (Newtonsoft.Json.dll) deverá estar presente na pasta principal do projeto para que o programa possa compilar e ser executado corretamente!

### Download:

##### Para compilar o programa, basta seguir os passos abaixo (utilizando o mono):

    git clone https://github.com/Wolfterro/IPSharp.git
    cd IPSharp/
    mcs /out:IPSharp.exe /win32icon:Icon.ico src/Main.cs src/IPSharp.cs /r:./Newtonsoft.Json.dll
    ./IPSharp.exe

##### Caso queira compilar usando o csc no Windows, basta executar o arquivo Compile.bat que o script irá se encarregar de compilar o programa.
##### É necessário que o csc seja reconhecido como um comando interno no Prompt, caso contrário deverá digitar todo o caminho do compilador manualmente! 
