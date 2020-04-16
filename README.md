# One Dance (Versão 1) #

Iniciação Científica desenvolvida no Instituto Federal de Educação, Ciência e Tecnologia do Espírito Santo - Campus Colatina. Aplicação que, em conjunto com o dispositivo Kinect, identifica alguns movimentos realizados e os avalia como corretos ou não. Através deste feedback, são fornecidos dados para professores de orientação e mobilidade para que então possam criar rotinas de atividades para seus alunos. Nesta versão é utilizada a forma 'KeyFrame', criando frames específicos de cada posição dos movimentos para identificação em tempo real.

Observações:
- Não utiliza nenhum ORM.
- É necessário ter um dispositivo Kinect conectado a máquina.

Configurações:
- Configuração de dados de usuário/database/senha através do arquivo 'DatabaseConfiguration.json' em Debug/Release.
- Instalação de conectores mysql/net através dos executáveis em 'NetConnector/'.

Formas de uso:
- Criação da database que foi configurada no arquivo de configurações.
- Execução do arquivo 'Script.sql' em 'Database/Script.sql'.
- Execução normal do programa.
