var setCategory = function (category) {
    customerModel.selectedCategory(category);
    filterProductsByCategory();
}

var setView = function (view) {
    customerModel.currentView(view);
}

var addToCart = function (product) {
    var found = false;
    var cart = customerModel.cart();
    for (var i = 0; i < cart.length; i++) {
        if (cart[i].product.Id == product.Id) {
            found = true;
            count = cart[i].count + 1;
            //se o produto adicionado já existe no carrinho, incrementa a quantidade,
            //remove o produto do carrinho (splice)
            //e o readiciona com a quantidade incrementada
            customerModel.cart.splice(i, 1);
            customerModel.cart.push({
                count: count,
                product: product
            });
            break;
        }
    }
    if (!found) {
        customerModel.cart.push({ count: 1, product: product });
    }

    setView("cart");
}

var removeFromCart = function (productSelection) {
    customerModel.cart.remove(productSelection);
}

var placeOrder = function () {
    var order = {
        Customer: model.username(),
        //map retorna um array de objetos {count,productid} com base no array cart()
        Lines: customerModel.cart().map(function (item) {
            return {
                Count: item.count,
                ProductId: item.product.Id
            }
        })
    };

    saveOrder(order, function () {
        setView("thankyou");
    });
}

model.products.subscribe(function (newProducts) {
    filterProductsByCategory();

    customerModel.productCategories.removeAll();
    //adiciona os elementos de um array a productCategories
    customerModel.productCategories.push.apply(customerModel.productCategories,
        model.products().map(function (p) {
            return p.Category;
        })
        //Itera sobre o array e para cada elemento checa se a primeira posição do elemento no array é igual à posição atual.
        //Para elementos duplicados as posições serão diferentes
        .filter(function (value, index, self) {
            //se verdadeiro o elemento entra no array filtrado
            return self.indexOf(value) === index;
        }).sort());
});

//subscribe atribui uma função de callback ao success de um observable
customerModel.cart.subscribe(function (newCart) {
    //reduce soma incrementando um valor ao prev (valor inicial ou último retornado pela função)
    customerModel.cartTotal(newCart.reduce(
        function (prev, item) {
            return prev + (item.count * item.product.Price);
        }, 0));

    customerModel.cartCount(newCart.reduce(
        function (prev, item) {
            return prev + item.count;
        }, 0));
});

var filterProductsByCategory = function () {
    var category = customerModel.selectedCategory();

    customerModel.filteredProducts.removeAll();
    customerModel.filteredProducts.push.apply(customerModel.filteredProducts,
        model.products().filter(function (p) {
            return category == null || p.Category == category;
        }));
}

$(document).ready(function () {
    getProducts();
})