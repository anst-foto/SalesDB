-- CREATE DATABASE sales_db;
-- CREATE SCHEMA sales_db.test;
-- set search_path = "test"

CREATE TABLE table_products (
	id SERIAL NOT NULL PRIMARY KEY,
	name TEXT NOT NULL UNIQUE,
	price DECIMAL NOT NULL CHECK ( price > 0 )
);

CREATE TABLE table_sales (
	id BIGSERIAL NOT NULL PRIMARY KEY,
	product_id INTEGER NOT NULL,
	date DATE NOT NULL DEFAULT current_date,
	amount SMALLINT NOT NULL DEFAULT 1 CHECK ( amount > 0 ),
    FOREIGN KEY (product_id) REFERENCES table_products (id)
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

CREATE VIEW view_sales AS
    SELECT table_sales.id AS id,
           table_sales.date AS date,
           table_products.name AS product_name,
           table_products.price AS price,
           table_sales.amount AS amount
    FROM table_sales
        JOIN table_products
            ON table_sales.product_id = table_products.id;

INSERT INTO table_products (name, price)
VALUES ('product_1', 10),
       ('product_2', 20),
       ('product_3', 30);

INSERT INTO table_sales (product_id, date, amount)
VALUES (1, '2020-01-01', 10),
       (2, '2020-01-02', 20),
       (3, '2020-01-03', 30);

CREATE PROCEDURE procedure_delete_product(IN product_name TEXT)
    LANGUAGE SQL
AS $$
    DELETE FROM table_products WHERE name = product_name;
$$;

-- CALL procedure_delete_product('product_4');
