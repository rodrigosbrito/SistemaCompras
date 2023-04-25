# SistemaCompras 

O projeto Prova.SISPREV consiste em uma API simples para os objetos Produto e SolicitacaoCompra. 

### Atenção: Código fonte da solução se encontra na Branch *develop*

## Objetivo da Prova

Criar um método, que tem o objetivo de inserir um novo registro no banco de dados. De acordo com o contexto do teste, esse método vai inserir um novo Registro de Compra.

### Regras de Negócio:
1.	Se o Total Geral for maior que 50000 a condição de pagamento deve ser 30 dias.
2.	O total de itens de compra deve ser maior que 0.

### Request 1
Request utilizada com uma solicitação com Total geral < 50000 e registrando uma Condicao de Pagamento nula:
![Swagger Request Solicitacao 1](https://github.com/rodrigosbrito/SistemaCompras/blob/develop/prints/request_swagger_create_solicitacao.PNG?raw=true)

### Request 2
Request utilizada com uma solicitação com Total geral > 50000 e registrando uma Condicao de Pagamento de 30 dias:
![Swagger Request Solicitacao 2](https://github.com/rodrigosbrito/SistemaCompras/blob/develop/prints/request_swagger_create_solicitacao_2.PNG?raw=true)

### Banco de Dados
Registro das duas inserções no banco atendendo a RN1:
![Banco de dados](https://github.com/rodrigosbrito/SistemaCompras/blob/develop/prints/sql_query_solicitacoes.PNG?raw=true)
