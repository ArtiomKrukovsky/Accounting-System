-- STREAMS
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
  WITH (kafka_topic='Confectionery', value_format='json');

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