# WeatherApiService
Serviço para consumo da api http://api.openweathermap.org/data/2.5/

Ocorreu problemas com o projeto para publicação no azure, de referencias com as packages do nuget, que não consegui arrumar a tempo;

Para acessar a api via swagger, será necessário fazer checkout do fonte, abrir a solution em um visual studio 2019, rebuildar, e disparar no IIS a api, onde será direcionado pra url do swagger automaticamente.

Infelizmente ainda não estão se comunicando via message broker (rabbitMQ, etc), e sim diretamente, mas futuramente vou implementar essa parte.
