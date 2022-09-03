import classes from './ShoppingCart.module.css';
import React from 'react';
import CartItem, { CartItemObject } from './CartItem/CartItem';
import { ProductObject } from '../Products/Product/Product';

interface ShoppingCartProps {
  cart: Array<CartItemObject>;
  products: Array<ProductObject>;
  deleteFromCart: (addedProductId: number) => void;
}

export default function ShoppingCart(props: ShoppingCartProps) {
  let cartTotalPrice = 0;

  const couponDiscount = 1;

  const cartElements = props.cart.map((item) => {
    const product = props.products.find((product) => {
      return product.id === item.product_id;
    });

    if (product == null) {
      return;
    }

    cartTotalPrice += item.totalPrice;

    return (
      <CartItem
        key={item.product_id}
        {...item}
        product={product}
        deleteFromCart={props.deleteFromCart}
      />
    );
  });

  cartTotalPrice -= couponDiscount;

  return (
    <div className={classes.ShoppingCart}>
      <h3 className={classes.Title}>
        Siparişleriniz <span>({props.cart.length})</span>
      </h3>

      <div className={classes.MiddleWrapper}>
        <div className={classes.CartItemsContainer}>{cartElements}</div>
        <div className={classes.CouponDetails}>
          İndirim kuponu <span>1₺</span>
          <div>
            <span>erciyes-bilgisayar</span>
          </div>
        </div>
        <div className={classes.TotalPrice}>
          Toplam fiyat <span>{cartTotalPrice}₺</span>
        </div>
      </div>

      <div className={classes.CouponWrapper}>
        <input type="text" placeholder="İndirim Kuponu"></input>
        <button>Kupon Gir</button>
      </div>
    </div>
  );
}
