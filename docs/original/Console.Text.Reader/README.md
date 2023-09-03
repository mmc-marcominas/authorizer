# Desafio authorizer

Este documento descreve a solução implementada pelo desafio enviado no 
processo de seleção.

Importante: até o presente momento a solução está funcional - todas as 
regras foram atendidas no entanto, a lógica que aplica as regras de negócio 
não está da forma mais adequada para criação dos testes unitários.

Próximos passos serão refatorar e fazer os testes unitários.

## Informações obrigatórias

As seguintes informações foram determinadas como obrigatórias nesse documento:

* Explicação sobre as decisões técnicas e arquiteturais do seu desafio.
* Justificativa para o uso de frameworks ou bibliotecas (se usadas).
* Instruções sobre como compilar e executar o projeto.
* Notas adicionais que você considere importantes para a avaliação.

## Decisões técnicas

As decisões técnicas foram tomadas buscando respeitar os conceitos de Kiss e 
 atender às recomendações contidas na PEP 20 do Python também conhecida como 
 The Zen of Python. Referências: 

* KISS - [Keep It Simple, Sir](https://en.wikipedia.org/wiki/KISS_principle)
* Zen of Python [PEP 20](https://www.python.org/dev/peps/pep-0020/)

## Justificativa para o uso de frameworks

Como os princípios podem ser usados em qualquer framework e pessoalmente 
ainda não tinha implementado algo assim usando o Framework .NET Core, decidi 
em faze-lo usando C# e o mínimo possível de bibliotecas externas mantendo em 
mente que [reiventar a roda deve ser evitada também](https://idioms.thefreedictionary.com/reinvent+the+wheel).

## Como compilar e executar o projeto

Esta seção do documento instrui como compilar e executar o projeto.

### Como compilar

* vá até a pasta `src\Console.Text.Reader` onde está o `.csproj` 
* digite o comando `dotnet build`

Caso você esteja em outro nível como `src\..`, por exemplo, use a seguinte
sintaxe: `dotnet build [caminho_do_arquivo_csproj]`, exemplo:
`dotnet build src\Console.Text.Reader\Console.Text.Reader.csproj`

O resultado esperado é algo como o texto a seguir:

``` text
[pasta_do_README.md]dotnet build src\Console.Text.Reader\Console.Text.Reader.csproj
Microsoft(R) Build Engine versão 16.9.0+57a23d249 para .NET
Copyright (C) Microsoft Corporation. Todos os direitos reservados.

  Determinando os projetos a serem restaurados...
  Todos os projetos estão atualizados para restauração.
  Console.Text.Reader -> [pasta_do_README.md]\src\Console.Text.Reader\bin\Debug\net5.0\Console.Text.Reader.exe

Compilação com êxito.
    0 Aviso(s)
    0 Erro(s)

Tempo Decorrido 00:00:00.99
```

Em um Linux o resultado esperado é algo como:

``` text
user@note:~/Projects/authorizer$
user@note:~/Projects/authorizer$ dotnet build src/Console.Text.Reader/Console.Text.Reader.csproj
Microsoft (R) Build Engine version 16.11.0+0538acc04 for .NET
Copyright (C) Microsoft Corporation. All rights reserved.

  Determining projects to restore...
  All projects are up-to-date for restore.
  Console.Text.Reader -> /mnt/Data/Projects/authorizer/src/Console.Text.Reader/bin/Debug/net5.0/Console.Text.Reader.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:02.67
```

### Como executar

* vá até a pasta `src` onde está a pasta `data` e as do projeto
* digite o comando `cat` ou `type` de acordo com o shell que desejar
* use o pipe `\` e indique o destino do executável

Por exemplo, no Developer Command, o resultado esperado é algo como:

``` text
D:\Dados\Projetos\authorizer\src>type data\operations.json | Console.Text.Reader\bin\Debug\net5.0\Console.Text.Reader.exe

Received data:
  {"account": {"active-card": true, "available-limit": 100}}
  {"transaction": {"merchant": "Burger King", "amount": 20, "time": "2019-02-13T10:00:00.000Z"}}
  {"transaction": {"merchant": "Habbib's", "amount": 90, "time": "2019-02-13T11:00:00.000Z"}}
  {"transaction": {"merchant": "McDonald's", "amount": 30, "time": "2019-02-13T12:00:00.000Z"}}

Done reading 4 lines and 336 of content length ;)
Insuficient limit error: Habbib's, 90, 13/02/2019 11:00:00
D:\Dados\Projetos\authorizer\src>
```

Em um shell Linux como Ubuntu, por exemplo, é algo como:

``` bash
user@note:~/Projects/authorizer$ cat ./src/data/operations.json | ./src/Console.Text.Reader/bin/Debug/net5.0/Console.Text.Reader

Received data:
  {"account": {"active-card": true, "available-limit": 100}}
  {"transaction": {"merchant": "Burger King", "amount": 20, "time": "2019-02-13T10:00:00.000Z"}}
  {"transaction": {"merchant": "Habbib's", "amount": 90, "time": "2019-02-13T11:00:00.000Z"}}
  {"transaction": {"merchant": "McDonald's", "amount": 30, "time": "2019-02-13T12:00:00.000Z"}}

Done reading 4 lines and 336 of content length ;)
Insuficient limit error: Habbib's, 90, 02/13/2019 11:00:00marcominas@mmc-note:
~/Projects/authorizer$
```

Observação, nem todos os detalhes do teste foram implementados e por isso, o 
resultado exibido nessa seção podem divergir da atual. Procurarei ficar com 
isso em mente e atualizar essa parte da documentação quando necesário.

## Notas adicionais

A versão desse documento foi escrita em português para mitigar possibilidade 
de equívocos pois não me considero fluente em inglês. Dito isso, entendo que 
a boa prática sugere usar o idioma inglês no código e na documentação para 
mitigar problemas com códigos específicos de um idioma, como acentuações, por 
exemplo.

O desenvolvimento vai seguir os seguintes passos:

* fazer o básico de cada etapa, como, por exemplo, implementar um console 
que receba dados no `std-in` e envie respostas no `std-out` por padrão e 
erros no `std-err` por boa prática.
* aplicar as regras especificadas no desafio
* fazer os testes unitários

Porque essa abordagem? Acredito que uma aplicação com testes unitários que 
não faz o que se espera e menos importante que uma aplicação que faz o que 
se espera dela. Não estou com isso querendo dar a impressão de que os testes 
podem ser desconsiderados mas deixando claro que entregar o que se espera é 
mais importante do meu ponto de vista. O resto, o código e os commits irão 
contar a história por trás do resultado entregado.
