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
```

###  Adım 2: RabbitMQ'yu Docker ile Başlatın
RabbitMQ'yu Docker üzerinde başlatmak için aşağıdaki komutu çalıştırın:

```bash
docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

Bu komut, RabbitMQ'yu localhost:5672 üzerinde başlatır ve yönetim panelini http://localhost:15672 adresinde açar.

### Adım 3: Projeyi Çalıştırın
Projeyi çalıştırmak için aşağıdaki komutları kullanın:

```bash
dotnet restore
dotnet run
```
Uygulama, http://localhost:5000 veya https://localhost:5001 adresinde çalışacaktır.

### Yapılandırma
Projenin yapılandırma ayarları appsettings.json dosyasında bulunur. Örnek yapılandırma:

```json
{
  "RabbitMQ": {
    "HostName": "localhost",
    "QueueName": "orders"
  },
  "Database": {
    "UseInMemoryDatabase": true,
    "ConnectionString": "Data Source=:memory:"
  }
}
```
RabbitMQ: RabbitMQ sunucusunun adresi ve kuyruk adı.

Database: Veritabanı yapılandırması. UseInMemoryDatabase true ise In-Memory Database kullanılır.

### Testler

Projede temel işlevler için unit testler bulunmaktadır. Testleri çalıştırmak için:

```bash
dotnet test
```
