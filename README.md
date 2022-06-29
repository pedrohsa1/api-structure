![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white) ![Visual Studio](https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white) ![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=Swagger&logoColor=white)

## Projeto API
O projeto EF.API é uma estrutura base que implementa os principais padrões de projetos e auxilia no desenvolvimento de APIs.

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


- Obter todas as Pessoas

`GET /api/v1/person/get-all`
