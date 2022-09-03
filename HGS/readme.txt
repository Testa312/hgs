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