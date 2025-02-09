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
