-- allazei oles tis apodeikseis apo id:3 se id:2
update invoices_group
set invoice_type = 2
where invoice_type >= 3 ;

-- allazei ola ta timologia apo id:1,2 se id:1
update invoices_group
set invoice_type = 1
where invoice_type < 3 ;


-- svinei to constraint gia na mporesoume na svhsoume ton pinaka
alter table invoices_group drop fk_invoice_type

-- svinei ton pinaka
drop table invoice_types

-- dimiourgei ton pinaka
create table invoice_types(
id int NOT NULL identity(1,1) primary key,
label nvarchar(400) NOT NULL,
name nvarchar(400) NOT NULL,
[current] int NOT NULL,
is_PrePrint int
) ;


-- insert dedomenon ston pinaka
insert into invoice_types(label,name,[current],is_PrePrint)
values('timologio','���������', 1,null) ;

insert into invoice_types(label,name,[current],is_PrePrint)
values('lianiki','�������� ��������',1,null) ;


-- ksana vazei to constraint sto invoices_group
alter table invoices_group
add constraint fk_invoice_type_id
foreign key (invoice_type)
references invoice_types(id) ;


-- vazei sto invoices_group tin kolona user_comments
alter table invoices_group
add user_comments nvarchar(400) ;
