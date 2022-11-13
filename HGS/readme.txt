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
2022.9.14 文件导入只支持从sis导出的统计csv文件。
2022.10.9 增加跳变和波动报警。
2022.10.29
----------------
	相关性是打开数据的钥匙，统计点石成金。吴军－智能时代。
	同一台，同一台机组的许多参数具有相关性，用相关性进行异常参数识别是不条可行的办法。
2022.11.4------
设备运行参数相关性报警，用矢量的模规范化为相同长度，再用DTW进行波形比较。
2022.11.6 oxyplot安装时出现 冲突，在nuGet卸载相应模块即可。
2022-11-13-----------------
dtaidistance DTW 配置max_dist,use_pruning参数后速度很快。