import logo from './bm.png';
import classes from './App.module.css';
import React, { Component } from 'react';
import ShoppingCart from './components/ShoppingCart/ShoppingCart';
import Products from './components/Products/Products';
import { ProductObject } from './components/Products/Product/Product';
import { CartItemObject } from './components/ShoppingCart/CartItem/CartItem';

interface ShopProps {}

interface ShopState {
  products: Array<ProductObject>;
  cart: Array<CartItemObject>;
}

class App extends Component<ShopProps, ShopState> {
  state = {
    products: [
      {
        id: 1,
        name: 'Ekmek',
        price: 2,
        unit: 'adet',
        explanation: 'Somun ekmek',
        quantity: 1,
        weight: '200 gr',
      },
      {
        id: 2,
        name: 'Çikolata',
        price: 5,
        unit: 'adet',
        quantity: 1,
        explanation: 'Bitter çikolata',
        weight: '60 gr',
      },
      {
        id: 3,
        name: 'Domates',
        price: 6,
        unit: 'kg',
        explanation: 'Domates',
        quantity: 1,
        weight: 'Antalya Sera',
      },
    ],
    cart: [
      {
        product_id: 1,
        quantity: 1,
        totalPrice: 2,
      },
    ],
  };

  addProductToCartHandler = (addedProductId: number) => {
    console.log(this.state.cart);

    const productIndexInCart = this.state.cart.findIndex((cartItem) => {
      return cartItem.product_id === addedProductId;
    });

    const addedProduct = this.state.products.find((product) => {
      return product.id === addedProductId;
    });

    if (addedProduct == null) {
      return;
    }

    if (productIndexInCart === -1) {
      // First time added

      const updatedCart = [...this.state.cart];

      let newCartItem = {
        product_id: addedProductId,
        quantity: 1,
        totalPrice: addedProduct.price,
      };

      updatedCart.push(newCartItem);

      this.setState({
        cart: updatedCart,
      });

      return;
    }

    const updatedCart = [...this.state.cart];

    const oldCartItem = this.state.cart[productIndexInCart];
    let newCartItem = { ...oldCartItem };

    newCartItem = {
      product_id: oldCartItem.product_id,
      quantity: oldCartItem.quantity + 1,
      totalPrice: oldCartItem.totalPrice + addedProduct.price,
    };

    updatedCart[productIndexInCart] = newCartItem;

    this.setState({
      cart: updatedCart,
    });
  };

  deleteProductFromCartHandler = (deletedProductId: number) => {
    const productIndexInCart = this.state.cart.findIndex((cartItem) => {
      return cartItem.product_id === deletedProductId;
    });

    const deletedProduct = this.state.products.find((product) => {
      return product.id === deletedProductId;
    });

    if (deletedProduct == null) {
      return;
    }

    const updatedCart = [...this.state.cart];

    updatedCart.splice(productIndexInCart, 1);

    this.setState({
      cart: updatedCart,
    });
  };

  render() {
    return (
      <div className={classes.App}>
        <img src={logo} alt="" />
        <h2
          style={{
            marginBottom: '0',
          }}
        >
          Erciyes Üniversitesi Bilgisayar Mühendisliği Bölümü Web Programlama 1
          dersi Final sorusu
        </h2>
        <h2
          style={{
            margin: '0',
          }}
        >
          Dr. Öğr. Üyesi Fehim KÖYLÜ
        </h2>
        <h1
          style={{
            margin: '0',
          }}
        >
          Alışveriş Uygulaması
        </h1>
        <p>
          Vermek istediğiniz siparişler için aşağıdaki listeden seçerek adedini
          belirtiniz.
        </p>

        <Products
          products={this.state.products}
          addToCart={this.addProductToCartHandler}
        />
        <ShoppingCart
          cart={this.state.cart}
          products={this.state.products}
          deleteFromCart={this.deleteProductFromCartHandler}
        />

        <footer className={classes.Footer}>
          <div>
            © Alper Güven - Erciyes Üniversitesi Bilgisayar Mühendisliği Bölümü
          </div>
          <div className={classes.FooterLinks}>
            <a href="/">Bölüm sayfası</a>
            <a href="/">Alışveriş Kuralları</a>
            <a href="/">Destek</a>
          </div>
        </footer>
      </div>
    );
  }
}

export default App;
