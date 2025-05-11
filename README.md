Health&Med

## ✅ Requisitos Funcionais (RF)

RF01 - Agendamento de Consultas
O sistema deve permitir que pacientes agendem consultas com médicos disponíveis em dias e horários específicos.

RF02 - Cancelamento e Reagendamento
Pacientes e médicos devem poder cancelar ou reagendar consultas com antecedência.

RF03 - Histórico de Consultas
O sistema deve manter o histórico de consultas realizadas por paciente e por médico.

RF04 - Acesso Seguro aos Dados
Os dados dos pacientes e de consultas devem ser acessíveis apenas por usuários autorizados.

RF05 - Login e Autenticação de Usuário
O sistema possui um mecanismo de autenticação para acesso por médicos e pacientes.

RF06 - Fila de Processamento Assíncrona
O sistema processa notificações e atualizações de agenda via fila (RabbitMQ), desacoplando essas tarefas da interface principal.

## ✅ Requisitos Não Funcionais (RNF)

RNF01 - Escalabilidade
A arquitetura do sistema é escalável horizontalmente, permitindo a adição de novos serviços conforme a demanda crescer.

RNF02 - Performance
O tempo de resposta do sistema é inferior a 2 segundos para 95% das requisições.

RNF03 - Conformidade com LGPD
O sistema criptografa dados sensíveis de pacienetes e médicos.

RNF04 - Observabilidade
Logs, métricas e monitoramento foram implementados para garantir visibilidade sobre o funcionamento do sistema.

## 🧩 **Justificativas Técnicas da Arquitetura**
### 🔹 Linguagem e Plataforma
.NET 8: escolhida pela robustez, suporte a APIs modernas, alta performance com WebAPI minimalista, e facilidade de manutenção.

ASP.NET Core WebAPI: Para expor os serviços RESTful e manter o backend desacoplado do front-end.

### 🔹 Banco de Dados
SQL Server: banco relacional robusto e confiável para manter consistência de dados críticos como registros de pacientes, médicos e consultas.

### 🔹 Mensageria
RabbitMQ: utilizado para garantir o processamento assíncrono de tarefas como envio de e-mails e notificações, melhorando a performance e escalabilidade do sistema.

### 🔹 Microsserviço
Uma arquitetura orientada a serviços, separando os domínios de “Agendamento”, “Médico” e “Paciente”.

### 🔹 Deploy e Escalabilidade
Containers (Docker): facilitam o deploy em ambientes padronizados.

Kubernetes : para orquestração em produção.

### 🔹 Segurança
Autenticação via JWT

Restrições de acesso por perfil de usuário

## Arquitetura da solução:

![Screenshot_1](https://github.com/user-attachments/assets/38d63ece-34a4-4eec-bf89-a55655d75a29)
