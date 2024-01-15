Documentation - Configuration d'un Serveur Web avec ASP.NET, Nginx, et Apache
Introduction

Cette documentation fournit des instructions simples et claires pour mettre en place un serveur web en utilisant les technologies ASP.NET, Nginx, et Apache. Suivez ces étapes pour déployer votre application web.
ASP.NET
1. Installation d'ASP.NET

Installez ASP.NET sur votre serveur en suivant les étapes ci-dessous :

bash

sudo apt update
sudo apt install -y aspnetcore-runtime

2. Déploiement de l'Application ASP.NET

Placez votre application ASP.NET dans le répertoire approprié, puis exécutez les commandes suivantes :

bash

cd /chemin/vers/votre/application
dotnet publish -c Release -o /var/www/aspnet

3. Configuration d'ASP.NET avec Nginx

Installez Nginx et configurez-le pour rediriger les requêtes vers votre application ASP.NET :

bash

sudo apt install -y nginx
sudo nano /etc/nginx/sites-available/default

Modifiez la configuration Nginx pour inclure les lignes suivantes :

nginx

server {
    listen 80;
    server_name votre_domaine.com www.votre_domaine.com;

    location / {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
    }
}

Redémarrez Nginx pour appliquer les changements :

bash

sudo systemctl restart nginx

Apache
1. Installation d'Apache

Installez Apache sur votre serveur :

bash

sudo apt update
sudo apt install -y apache2

2. Configuration d'Apache pour ASP.NET

Activez le module proxy d'Apache et configurez-le pour rediriger les requêtes vers votre application ASP.NET :

bash

sudo a2enmod proxy
sudo a2enmod proxy_http
sudo nano /etc/apache2/sites-available/000-default.conf

Ajoutez les lignes suivantes dans la section <VirtualHost> :

apache

<VirtualHost *:80>
    ServerName votre_domaine.com

    ProxyPreserveHost On
    ProxyPass / http://localhost:5000/
    ProxyPassReverse / http://localhost:5000/

    ErrorLog ${APACHE_LOG_DIR}/error.log
    CustomLog ${APACHE_LOG_DIR}/access.log combined
</VirtualHost>

Redémarrez Apache pour appliquer les changements :

bash

sudo systemctl restart apache2
