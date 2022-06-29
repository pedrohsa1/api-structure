## Projeto API
O projeto EF.API é uma estrutura base implementado os principais padrões de projetos que auxilia no desenvolvimento de APIs. Nesta estrutura estamos utilizando.

## Endpoints

- Autenticação

`POST /api/v1/auth/login`

Corpo da requisição
```sh
{
  "username": "string",
  "password": "string"
}
```

- Cadastrar Pessoa

`POST /api/v1/person/create`

Corpo da requisição
```sh
{
  "code": "string",
  "name": "string",
  "cpf": "string",
  "uf": "string",
  "birthDate": "string"
}
```


- Atualizar Pessoa

`PUT /api/v1/person/update`

Corpo da requisição
```sh
{
  "id": 0,
  "code": "string",
  "name": "string",
  "cpf": "string",
  "uf": "string",
  "birthDate": "string"
}
```

- Atualizar Pessoa

`DELETE /api/v1/person/remove/{id}`


- Obter Pessoa pelo id

`GET /api/v1/person/{id}`


- Obter Pessoa pelo código

`GET /api/v1/person/code/{code}`


- Obter Pessoa(s) pela UF

`GET /api/v1/person/uf/{uf}`


- Obter Pessoa(s) pela UF

`GET /api/v1/person/uf/{uf}`


- Obter todas as Pessoas

`GET /api/v1/person/get-all`
