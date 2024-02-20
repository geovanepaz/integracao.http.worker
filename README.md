<h1 align="center">Geovane Paz - Backend Developer</h1>


## Objetivo
É criar um exemplo de arquitetura para worker utilizando chamada HTTP com .Net 7, Dapper, HttpClient, Records e ScopeFactory. 

##Projeto com apenas duas camadas que serve para efetuar consultas no banco e direcionar para a API de contexto responsável, tornando o serviço mais leve e sem regras.

#Trechos de código

**1 - Dapper**:

Uma abordagem mais eficiente para inicializar o Dapper e consumir menos memória em contêineres Linux. Essa estratégia visa eliminar o uso de 'using', tornando o código mais limpo, e transferir a responsabilidade para a injeção de dependência.

**Maneira tradicional:**

![image](https://github.com/geovanepaz/integracao.http.worker/assets/16936418/5f98c429-6ff2-4395-befc-99ed207af84f)

**Via injeção de dependência:**

![image](https://github.com/geovanepaz/integracao.http.worker/assets/16936418/f7df745d-2b02-4420-93ac-dab256ccbd7e)

**Inicialização na Program ou IoC **
![image](https://github.com/geovanepaz/integracao.http.worker/assets/16936418/d227ad92-606e-4ac8-b5c9-32e4cf36d00b)


