import React from 'react';
import classes from './Product.module.css';

export interface ProductProps extends ProductObject {
  addToCart: (addedProductId: number) => void;
}

export interface ProductObject {
  id: number;
  name: string;
  price: number;
  unit: string;
  explanation: string;
  quantity: number;
  weight?: string;
}

export default function Product(props: ProductProps) {
  return (
    <div className={classes.Product}>
      <div className={classes.ProductName}>{props.name}</div>
      <div className={classes.ProductDetails}>
        <div
          style={{
            fontSize: '1.30rem',
          }}
        >
          <span
            style={{
              fontWeight: 'bold',
            }}
          >
            {props.price} ₺
          </span>{' '}
          / {props.unit}
        </div>
        <div>{props.explanation}</div>
        <div>{props.weight}</div>
        <button
          className={classes.AddToCart}
          onClick={() => props.addToCart(props.id)}
        >
          Sepete Ekle
        </button>
      </div>
    </div>
  );
}
