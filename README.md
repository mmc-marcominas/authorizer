# Desafio authorizer

Este projeto descreve a solução implementada para um desafio de um processo 
de seleção que participei e que recentemente revisitei para refatorar usando 
extension methods para agrupar funcionalidades de acordo com determinados
tipos de objetos usados.

Detalhes do desafio podem ser lidos em [challenge.md](docs/challenge.md)

Detalhes da [solução original](docs/original/):

|arquivo| descrição|
|---|---|
|[Commands/CustomerTransactionsCommand.md](docs/original/Console.Text.Reader/Commands/CustomerTransactionsCommand.md)|Implementação das regras|
|[Domain/Enums/ProcessStatus.md](docs/original/Console.Text.Reader/Domain/Enums/ProcessStatus.md)|Possíveis códigos de saída da aplicação|
|[Domain/Enums/ProcessViolations.md](docs/original/Console.Text.Reader/Domain/Enums/ProcessViolations.md)|Tipos de violações|
|[Domain/Enums/ProcessViolationsExtensions.md](docs/original/Console.Text.Reader/Domain/Enums/ProcessViolationsExtensions.md)|Métodos relacionado às violações|
|[Domain/Account.md](docs/original/Console.Text.Reader/Domain/Account.md)|Entidades conta|
|[Domain/CustomerTransactions.md](docs/original/Console.Text.Reader/Domain/CustomerTransactions.md)|Entidade transações do cliente|
|[Domain/Transaction.md](docs/original/Console.Text.Reader/Domain/Transaction.md)|Entidade transação|
|[Infra/RequiredDataNotFoundException.md](docs/original/Console.Text.Reader/Infra/RequiredDataNotFoundException.md)|Excessão dados não encontrados|
|[Properties/launchSettings.json](docs/original/Console.Text.Reader/Properties/launchSettings.json)|Launch settings da aplicação|
|[Dockerfile](docs/original/Console.Text.Reader/Dockerfile)|Dockerfile|
|[Program.md](docs/original/Console.Text.Reader/Program.md)|Implementação da Program.Main|
|[README.md](docs/original/Console.Text.Reader/README.md)|README original|
|||

## Informações obrigatórias

As seguintes informações foram determinadas como obrigatórias nesse documento:

* Explicação sobre as decisões técnicas e arquiteturais do seu desafio.
* Justificativa para o uso de frameworks ou bibliotecas (se usadas).
* Instruções sobre como compilar e executar o projeto.
* Notas adicionais importantes para a avaliação.

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
mente que [reiventar a roda](https://idioms.thefreedictionary.com/reinvent+the+wheel) deve ser evitada também.

## Como compilar e executar o projeto

O projeto conta com um [Makefile](Makefile) com instruções para 
compilar, publicar, executar e testar essa implementação.

### Como compilar

Execute o seguinte comando na pasta raiz que contém o arquivo [Makefile](Makefile):

``` bash
$ make build
```

O resultado esperado é algo como o texto a seguir:

```
Building solution...
MSBuild version 17.7.1+971bf70db for .NET
  Determining projects to restore...
  All projects are up-to-date for restore.
  authorizer -> ~/projects/authorizer/src/bin/Release/net6.0/authorizer.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:01.73
marcominas@DESKTOP-NLL30IN ~/projects/authorizer
```

### Como executar

O Makefile and usa [jq](https://jqlang.github.io/jq/) para formatar os arquivos json dos testes. Tente instalar `jq` se o seguinte erro ocorer na execução dos testes:

``` bash
$ make test
/bin/sh: 4: jq: not found
make: *** [Makefile:5: test] Error 127
```

Execute o seguinte comando na pasta raiz que contém o arquivo [Makefile](Makefile):

``` bash
$ make run json=operations.json
```
O resultado esperado é algo como:

``` bash
json: operations.json
{
  "account": {
    "active-card": true,
    "available-limit": 100
  }
}
{
  "transaction": {
    "merchant": "Burger King",
    "amount": 20,
    "time": "2019-02-13T10:00:00.000Z"
  }
}
{
  "transaction": {
    "merchant": "Habbib's",
    "amount": 30,
    "time": "2019-02-13T11:00:00.000Z"
  }
}
{
  "transaction": {
    "merchant": "McDonald's",
    "amount": 40,
    "time": "2019-02-13T12:00:00.000Z"
  }
}

result

{"account": {"active-card": true, "available-limit": 100}, "violations": []}
{"account": {"active-card": true, "available-limit": 80}, "violations": []}
{"account": {"active-card": true, "available-limit": 50}, "violations": []}
{"account": {"active-card": true, "available-limit": 10}, "violations": []}

Press enter to continue
```

Possíveis execuções disponíveis na pasta [data](data) até o momento:

| json | regra |
|---|---|
| operations.json | execução com sucesso |
| operations-account-already-initialized.json | erro conta já inicializada |
| operations-account-not-initialized.json | erro conta não inicializada |
| operations-card-not-active.json | erro cartão inativo |
| operations-doubled-transaction.json | erro transação duplicada |
| operations-empty.json | erro sem operação |
| operations-high-frequency-small-interval.json | erro alta frequência em pouco tempo |
| operations-insufficient-limit.json | fundo insuficiente |
| operations-multiple-rules-breaks.json | quebra de diversas regras |
|  |  |


## Notas adicionais

O desenvolvimento seguiu os seguintes passos:

* fazer o básico de cada etapa, como, por exemplo, implementar um console 
que receba dados no `std-in` e envie respostas no `std-out` por padrão e 
erros no `std-err` por boa prática.
* aplicar as regras especificadas no desafio
* testar cada regra

Qualquer novo teste pode ser feito seguindo as seguintes etapas:

 * criar um arquivo `.json` na pasta [data](data) com as instruções que 
   se deseja
 * alterar o arquivo [Makefile](Makefile) para adicionar a chamada para
   o arquivo criado
