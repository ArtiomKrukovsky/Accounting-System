CREATE STREAM orders (
	id VARCHAR, 
	title VARCHAR, 
	orderCreated DATE,
	orderStatus VARCHAR,
	orderItems ARRAY<
					STRUCT<
					orderItemId VARCHAR,
					pieId VARCHAR,
					unitPrice DOUBLE,
					units INTEGER,
					totalPrice DOUBLE,
					discount DOUBLE>>)
  WITH (kafka_topic='Confectionery', value_format='json', partitions=1);