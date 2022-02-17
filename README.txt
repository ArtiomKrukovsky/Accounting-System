-- STREAMS
a) Orders
CREATE STREAM orders (
	id VARCHAR, 
	title VARCHAR, 
	orderDate VARCHAR,
	orderStatus STRUCT<id INTEGER, name VARCHAR>,
	orderItems ARRAY<
					STRUCT<
					id VARCHAR,
                                        orderId VARCHAR,
					pieId VARCHAR,
					unitPrice DOUBLE,
					units INTEGER,
					totalPrice DOUBLE,
					discount DOUBLE>>)
  WITH (kafka_topic='orders', value_format='json');

- kafka_topic - Name of the Kafka topic underlying the stream.
- value_format - Encoding of the messages stored in the Kafka topic.
- partitions - Number of partitions to create for the locations topic. Note that this parameter is not needed for topics that already exist.

CREATE STREAM orders_denormalized AS
  SELECT id as id,
         title AS title,
         orderDate AS createdDate,
	 orderStatus->name AS status,
	 EXPLODE(orderItems)->id AS orderItemId,	
         EXPLODE(orderItems)->pieId AS pieId,
         EXPLODE(orderItems)->unitPrice AS unitPrice,
         EXPLODE(orderItems)->units AS units,
         EXPLODE(orderItems)->totalPrice AS totalPrice,
         EXPLODE(orderItems)->discount AS discount
  FROM orders
  EMIT CHANGES;

b) Pies
CREATE STREAM pies (
	id VARCHAR,
	name VARCHAR,
	description VARCHAR,
	portions STRUCT<minimum INTEGER, maximum INTEGER>,
	ingredients ARRAY<
					STRUCT<
					name VARCHAR,
					isAllergen BOOLEAN,
					relativeAmount DOUBLE>>)
  WITH (kafka_topic='pies', value_format='json');

CREATE STREAM pies_denormalized AS
  SELECT id as id,
	 name as name,
 	 description as description,
	 portions->minimum as minimumPortions,
         portions->maximum as maximumPortions,	
	 ingredients as ingredients
  FROM pies
  EMIT CHANGES;

CREATE TABLE pies_view AS
  SELECT id,
         LATEST_BY_OFFSET(name) as name,
         LATEST_BY_OFFSET(description) as description,
	 LATEST_BY_OFFSET(minimumPortions) as minimumPortions,
 	 LATEST_BY_OFFSET(maximumPortions) as maximumPortions,
	 COLLECT_LIST(CAST(ingredients as VARCHAR)) as ingredients 
  FROM pies_denormalized
  GROUP BY id
  EMIT CHANGES;
	
-- TABLES
CREATE TABLE orders_view AS
  SELECT id,
         LATEST_BY_OFFSET(title) as title,
         LATEST_BY_OFFSET(createdDate) as createdDate,
	 LATEST_BY_OFFSET(status) as status,
	 LATEST_BY_OFFSET(orderItemId) as orderItemId,	
         LATEST_BY_OFFSET(pieId) as pieId,
         LATEST_BY_OFFSET(unitPrice) as unitPrice,
         LATEST_BY_OFFSET(units) as units,
         LATEST_BY_OFFSET(totalPrice) as totalPrice,
         LATEST_BY_OFFSET(discount) as discount
  FROM orders_denormalized 
  GROUP BY id
  EMIT CHANGES;

CREATE TABLE orders_count AS
  SELECT id as id,
     COUNT(*) AS TOTAL 
  FROM orders_denormalized
  GROUP BY id
  EMIT CHANGES;