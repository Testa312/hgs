2022.7.24 解决公式的循环计算问题。将计点分为两类，一类为中间计算点只能使用原始sis点。第二类为最终计算点，可使用一类点和原始sis点。
考虑处理计算点坏点问题。
//2022.8.22
CREATE TABLE point (
  ID NOT NULL,
  symbol TEXT NOT NULL,
  price DOUBLE PRECISION NULL,
  day_volume INT NULL
);
pg增加扩展模块：CREATE EXTENSION module_name;
不计算就不会报警，有关联。
没有实现用户注册，可在数据库中增加，其中0为管理员。
//新建用户--------------------
insert into password (userid,pwd) values (1, crypt('this is a pwd source', gen_salt('bf',10)));
INSERT 0 1

输入错误的密码, 返回假

select crypt('this is a error pwd source', password)=password from userpwd where userid =1;
 ?column? 
----------
 f
(1 row)

输入正确的密码, 返回真

select crypt('this is a pwd source', password)=password from userpwd where userid =1;
 ?column? 
----------
 t
(1 row)
-------------
UPDATE owner SET password = crypt('123456', gen_salt('bf',10));
--------------------------------------
