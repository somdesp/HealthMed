Health&Med

✅ Requisitos Funcionais (RF)

RF01 - Cadastro de Pacientes
O sistema deve permitir o cadastro de pacientes com informações como nome, CPF, data de nascimento, e-mail, telefone e endereço.

RF02 - Cadastro de Médicos
O sistema deve permitir o cadastro de médicos com informações como nome, CRM, especialidade, e-mail e disponibilidade de horários.

RF03 - Agendamento de Consultas
O sistema deve permitir que pacientes agendem consultas com médicos disponíveis em dias e horários específicos.

RF04 - Cancelamento e Reagendamento
Pacientes e médicos devem poder cancelar ou reagendar consultas com antecedência.

RF05 - Notificações de Agendamento
O sistema deve enviar notificações por e-mail ou SMS ao paciente e ao médico após a criação, alteração ou cancelamento de uma consulta.

RF06 - Histórico de Consultas
O sistema deve manter o histórico de consultas realizadas por paciente e por médico.

RF07 - Registro de Prontuário Médico
Durante ou após uma consulta, o médico pode registrar observações, diagnósticos e prescrições no prontuário do paciente.

RF08 - Acesso Seguro aos Dados
Os dados dos pacientes e prontuários devem ser acessíveis apenas por usuários autorizados.

RF09 - Login e Autenticação de Usuário
O sistema deve possuir um mecanismo de autenticação para acesso por médicos, pacientes e administradores.

RF10 - Fila de Processamento Assíncrona
O sistema deve processar notificações e atualizações de agenda via fila (RabbitMQ), desacoplando essas tarefas da interface principal.

✅ Requisitos Não Funcionais (RNF)

RNF01 - Escalabilidade
A arquitetura do sistema deve ser escalável horizontalmente, permitindo a adição de novos serviços conforme a demanda crescer.

RNF02 - Performance
O tempo de resposta do sistema deve ser inferior a 2 segundos para 95% das requisições.

RNF03 - Segurança de Dados
Todas as comunicações devem usar HTTPS. Dados sensíveis devem ser criptografados em repouso e em trânsito.

RNF04 - Conformidade com LGPD
O sistema deve garantir o consentimento do paciente para uso de seus dados e permitir a exclusão mediante solicitação.

RNF05 - Backup e Recuperação
O sistema deve realizar backups diários e permitir recuperação em caso de falhas.

RNF06 - Observabilidade
Logs, métricas e monitoramento devem ser implementados para garantir visibilidade sobre o funcionamento do sistema.

🧩 Justificativas Técnicas da Arquitetura
🔹 Linguagem e Plataforma
.NET 8: escolhida pela robustez, suporte a APIs modernas, alta performance com WebAPI minimalista, e facilidade de manutenção.

ASP.NET Core WebAPI: Para expor os serviços RESTful e manter o backend desacoplado do front-end.

🔹 Banco de Dados
SQL Server: banco relacional robusto e confiável para manter consistência de dados críticos como registros de pacientes, médicos e consultas.

🔹 Mensageria
RabbitMQ: utilizado para garantir o processamento assíncrono de tarefas como envio de e-mails e notificações, melhorando a performance e escalabilidade do sistema.

🔹 Microsserviço
Uma arquitetura orientada a serviços, separando os domínios de “Agendamento”, “Notificação”, “Autenticação”, etc.

🔹 Deploy e Escalabilidade
Containers (Docker): facilitam o deploy em ambientes padronizados.

Kubernetes : para orquestração em produção.

🔹 Segurança
Autenticação via JWT

Comunicação segura com HTTPS

Restrições de acesso por perfil de usuário
