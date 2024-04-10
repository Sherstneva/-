drop table Login

create table Login(
ID_user int identity(1,1) not null,
login_user nvarchar(50) not null,
password_user nvarchar(50) not null,
is_admin bit,
Role nvarchar(50) not null,
Real_name nvarchar(50) not null
)

select * from Login

insert into Login (login_user, password_user, is_admin, Role, Real_name) values ('admin', 'admin', 1, 'Заведующий-провизор', 'Михаил Григориевич')
