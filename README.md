# EclipseWorks

## Fase 1 - API Coding

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


## Fase 2  - Refinamento
O que você perguntaria para o PO visando o refinamento para futuras implementações ou melhorias.

 1. Sugeriria a implementação da feature  para consultar as Tarefas que estão à vencer e notificar o Usuário
 2. Implementação de mais Relatórios
 3. Estimativa de acesso de usuários e volumetria de criação e edtição de Projetos, Tarefas e Commentários para teste de carga
 4. Será necessário internacionalizar a aplicação para comportar outros idiomas?

## Fase 3 - Final

Melhoraria no projeto, identificando possíveis pontos de melhoria, implementação de padrões, visão do projeto sobre arquitetura/cloud, etc.

 1. Desenhar uma solução para verificar quais Tarefas estão à vencer e notificar o Usuário
 2. Utizar o AutoMapper
 3. Pegar as mensagem de validação do arquivo de configuração ao invés de ficar hadcoded
 4. Pegar o [número máximo de Tarefas por Projeto](https://github.com/BFPLima/EclipseWorks/blob/24df4e6a4985ae6d38bf41c6625516656c1d9be3/TaskManager.Service/TaskService.cs#L34) do arquivo de configuração ao invés de ficar hardcoded
