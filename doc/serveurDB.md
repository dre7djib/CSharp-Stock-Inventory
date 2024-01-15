## Server
- Créer une machine virtuelle avec [***ubuntu server***][https://ubuntu.com/server]


## Mysql

- ##### Installation de mysql

	``` bash
	sudo apt-get update
	sudo apt-get install mysql-server
	```


- ##### Verification du fonctionnement de mysql-server

	```bash
	sudo systemctl status mysql
	```


- ##### Entrer dans mysql
	``` bash
	sudo mysql -u root -p
	```
	Le sudo est important pour l'instant puisque si vous voulez vous accéder à mysql sans celui-ci vous pourrez ne pas.
	
	Pour fix l'access :
	```sql
	use mysql;
	update user set plugin='mysql_native_password' where user='root';
	flush privileges;
	```
	
	Vous pouvez maintenant accéder sans le sudo.
	```bash
	mysql -u root -p
	```


- ##### Créer la database 
	``` sql
	create database nom_de_la database;
	```
	Vous pouvez changer le nom de la database 


- ##### Création d'un utilisateur
	``` sql
	create user "utilisateur"@"%" identified by "mot_de_passe";
	exit;
	```


- ##### Connection pour les autres ordinateurs 
	```bash
	sudo nano /etc/mysql/mysql.conf.d/mysqld.cnf
	```
	Vous devez rajouter dans ce fichier un `#` sur la ligne `bind_address`

	```bash 
	sudo systemctl restart mysql
	```

	``` bash 
	hostname -I
	```
	Notez votre ipv4


- ##### Sur votre client 
	``` bash 
	mysql -h votre_ipv4 -u utlisateur -p
	```

	Vous pouvez aussi directement vous connecter sur un logiciel mysql en rentrant les informations nécessaires.