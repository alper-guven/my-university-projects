import classes from './Products.module.css';
import React from 'react';
import Product, { ProductObject, ProductProps } from './Product/Product';

interface ProductsProps {
  products: Array<ProductObject>;
  addToCart: (addedProductId: number) => void;
}

export default function Products(props: ProductsProps) {
  const productElements = props.products.map((product) => {
    return (
      <Product key={product.id} {...product} addToCart={props.addToCart} />
    );
  });

  return <div className={classes.Products}>{productElements}</div>;
}
