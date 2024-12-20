Welcome file
Welcome file


# EclipseWorks

## Fase 1

### Instruções para executar a aplicação

 1. Baixar o código fonte do [repositório](https://github.com/BFPLima/EclipseWorks)
 2. Entrar no diretório da Solution ***EclipseWorks.sln***. Lá vai estar o arquivo ***Dockerfile***
 3. Executar o comando ***docker build -t taskmanager-webapi -f Dockerfile .***
 4. Executar o commando ***docker create --name taskmanager-webapi  taskmanager-webapi***
 5. Executar o commando ***docker run -p 80:8080 -d taskmanager-webapi**
 
### Endpoint existentes

#### Projeto

#####  Criação

    curl --location 'http://localhost:80/api/project' \
    --header 'USER_ROLE: MANAGER' \
    --header 'Content-Type: application/json' \
    --data '{
        "Title":"Título do Projeto"
    }'

#####  Consutar

    curl --location 'http://localhost:80/api/project/ac62e523-969f-4334-82d9-7090ae503aed'


#####  Consutar Projetos por Usuário

    curl --location 'http://localhost:80/api/project/user/398e085a-70b3-45cd-83d2-6d06736f0638'

IDs dos Usuários:
Gerente: `ffc3859b-25a8-4834-99f6-f7f92f28ffda`
Analista:  `398e085a-70b3-45cd-83d2-6d06736f0638`

#####  Atualizar

    curl --location --request PUT 'http://localhost:80/api/project/ac62e523-969f-4334-82d9-7090ae503aed' \
    --header 'Content-Type: application/json' \
    --data '{
        "Title":"Título do Projeto atualiazado"
    }'

#####  Deletar
    curl --location --request DELETE 'http://localhost:80/api/project/398e085a-70b3-45cd-83d2-6d06736f0638'

####  Tarefa

#####  Criação

    curl --location 'http://localhost:80/api/task' \
    --header 'Content-Type: application/json' \
    --data '{
        "title":"Título da Tarefa",
        "description": "Descrição da Tarefa",
        "status": 2,
        "priority": 2,
        "dueDateTime":"2024-01-01T00:00:00",
        "projectId": "e60856b3-0928-4779-9227-c94453c63e11"
    }'

#####  Consultar por Projeto

      curl --location 'http://localhost:80/api/task/project/a357ccfe-7c5d-4d4f-9190-803ba4a9a7b5'

#####  Consultar Histórico de atualizações 

    curl --location 'http://localhost:5114/api/task/089051c8-fbec-4c44-b7b7-069f52c0a64c/history'

#####  Deletar

    curl --location --request DELETE 'http://localhost:80/api/task/94acceb2-d181-46f5-b6ba-8fb3d1249fa1'

#####  Atualizar

    curl --location --request PUT 'http://localhost:80/api/task/089051c8-fbec-4c44-b7b7-069f52c0a64c' \
    --header 'Content-Type: application/json' \
    --data '{
        "title":"Título da Tarefa 2",
        "description": "Descrição da Tarefa 2",
        "status": 2,
        "priority": 2,
        "dueDateTime":"2024-01-01T00:00:00"
    }'

####  Comentário

#####  Criação

    curl --location 'http://localhost:80/api/commentary' \
    --header 'Content-Type: application/json' \
    --data '{
        "comment":"Comentário da Tarefa",
        "taskId": "089051c8-fbec-4c44-b7b7-069f52c0a64c"
    }'

#####  Consutar

    curl --location 'http://localhost:80/api/commentary/93555df7-67f5-4520-92f4-cea4f8f8be94'

#####  Consutar por Tarefa

    curl --location 'http://localhost:80/api/commentary/task/23740d6b-ddac-4d79-a75a-e1ac20a86f0a'



EclipseWorks
Fase 1
Instruções para executar a aplicação
Baixar o código fonte do repositório
Entrar no diretório da Solution EclipseWorks.sln. Lá vai estar o arquivo Dockerfile
Executar o comando docker build -t taskmanager-webapi -f Dockerfile .
Executar o commando docker create --name taskmanager-webapi taskmanager-webapi
Executar o commando *docker run -p 80:8080 -d taskmanager-webapi
Endpoint existentes
Projeto
Criação
curl --location 'http://localhost:80/api/project' \
--header 'USER_ROLE: MANAGER' \
--header 'Content-Type: application/json' \
--data '{
    "Title":"Título do Projeto"
}'
Consutar
curl --location 'http://localhost:80/api/project/ac62e523-969f-4334-82d9-7090ae503aed'
Consutar Projetos por Usuário
curl --location 'http://localhost:80/api/project/user/398e085a-70b3-45cd-83d2-6d06736f0638'
IDs dos Usuários:
Gerente: ffc3859b-25a8-4834-99f6-f7f92f28ffda
Analista: 398e085a-70b3-45cd-83d2-6d06736f0638

Atualizar
curl --location --request PUT 'http://localhost:80/api/project/ac62e523-969f-4334-82d9-7090ae503aed' \
--header 'Content-Type: application/json' \
--data '{
    "Title":"Título do Projeto atualiazado"
}'
Deletar
curl --location --request DELETE 'http://localhost:80/api/project/398e085a-70b3-45cd-83d2-6d06736f0638'
Tarefa
Criação
curl --location 'http://localhost:80/api/task' \
--header 'Content-Type: application/json' \
--data '{
    "title":"Título da Tarefa",
    "description": "Descrição da Tarefa",
    "status": 2,
    "priority": 2,
    "dueDateTime":"2024-01-01T00:00:00",
    "projectId": "e60856b3-0928-4779-9227-c94453c63e11"
}'
Consultar por Projeto
  curl --location 'http://localhost:80/api/task/project/a357ccfe-7c5d-4d4f-9190-803ba4a9a7b5'
Consultar Histórico de atualizações
curl --location 'http://localhost:5114/api/task/089051c8-fbec-4c44-b7b7-069f52c0a64c/history'
Deletar
curl --location --request DELETE 'http://localhost:80/api/task/94acceb2-d181-46f5-b6ba-8fb3d1249fa1'
Atualizar
curl --location --request PUT 'http://localhost:80/api/task/089051c8-fbec-4c44-b7b7-069f52c0a64c' \
--header 'Content-Type: application/json' \
--data '{
    "title":"Título da Tarefa 2",
    "description": "Descrição da Tarefa 2",
    "status": 2,
    "priority": 2,
    "dueDateTime":"2024-01-01T00:00:00"
}'
Comentário
Criação
curl --location 'http://localhost:80/api/commentary' \
--header 'Content-Type: application/json' \
--data '{
    "comment":"Comentário da Tarefa",
    "taskId": "089051c8-fbec-4c44-b7b7-069f52c0a64c"
}'
Consutar
curl --location 'http://localhost:80/api/commentary/93555df7-67f5-4520-92f4-cea4f8f8be94'
Consutar por Tarefa
curl --location 'http://localhost:80/api/commentary/task/23740d6b-ddac-4d79-a75a-e1ac20a86f0a'
Markdown 3129 bytes 244 words 109 lines Ln 109, Col 0HTML 2415 characters 219 words 72 paragraphs
