# Order Management System

Bu proje, bir e-ticaret platformu için sipariş yönetim sistemi backend servisini içerir. Siparişler, RabbitMQ kuyruğu üzerinden işlenir ve REST API üzerinden sorgulanabilir.

## Teknolojiler

- **.NET Core 6+**
- **RabbitMQ** (Mesaj kuyruğu)
- **Entity Framework Core** (In-Memory Database)
- **RESTful API**
- **Docker** (RabbitMQ için)

## Kurulum

### Ön Koşullar

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Docker](https://www.docker.com/get-started) (RabbitMQ için)

### Adım 1: Projeyi Klonlayın
```bash
git clone https://github.com/aysgnl/OrderManagementSystem.git
cd OrderManagementSystem

###  Adım 2: RabbitMQ'yu Docker ile Başlatın
RabbitMQ'yu Docker üzerinde başlatmak için aşağıdaki komutu çalıştırın:

```bash
docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management
