import classes from './CartItem.module.css';
import React from 'react';
import { ProductObject } from '../../Products/Product/Product';

export interface CartItemProps extends CartItemObject {
  product: ProductObject;
  deleteFromCart: (addedProductId: number) => void;
}

export interface CartItemObject {
  product_id: number;
  quantity: number;
  totalPrice: number;
}

export default function CartItem(props: CartItemProps) {
  return (
    <div className={classes.CartItem}>
      <div>
        {props.product.name} ({props.quantity}) {props.totalPrice}₺
      </div>
      <button onClick={() => props.deleteFromCart(props.product_id)}>X</button>
    </div>
  );
}
