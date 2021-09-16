function initPayPalButton(dotNetHelper) {
    var shipping = 0;
    var itemOptions = document.querySelector("#smart-button-container #item-options");
    var quantity = parseInt();
    var quantitySelect = document.querySelector("#smart-button-container #quantitySelect");
    if (!isNaN(quantity)) {
        quantitySelect.style.visibility = "visible";
    }
    var orderDescription = '';
    if (orderDescription === '') {
        orderDescription = 'Package';
    }
    paypal.Buttons({
        style: {
            shape: 'rect',
            color: 'gold',
            layout: 'vertical',
            label: 'paypal',

        },
        createOrder: function (data, actions) {
            var selectedItemDescription = itemOptions.options[itemOptions.selectedIndex].value;
            var selectedItemPrice = parseFloat(itemOptions.options[itemOptions.selectedIndex].getAttribute("price"));
            var tax = (0 === 0 || false) ? 0 : (selectedItemPrice * (parseFloat(0) / 100));
            if (quantitySelect.options.length > 0) {
                quantity = parseInt(quantitySelect.options[quantitySelect.selectedIndex].value);
            } else {
                quantity = 1;
            }

            tax *= quantity;
            tax = Math.round(tax * 100) / 100;
            var priceTotal = quantity * selectedItemPrice + parseFloat(shipping) + tax;
            priceTotal = Math.round(priceTotal * 100) / 100;
            var itemTotalValue = Math.round((selectedItemPrice * quantity) * 100) / 100;

            return actions.order.create({
                purchase_units: [{
                    description: orderDescription,
                    amount: {
                        currency_code: 'PHP',
                        value: "270.00",
                        breakdown: {
                            item_total: {
                                currency_code: 'PHP',
                                value: "270.00",
                            },
                            shipping: {
                                currency_code: 'PHP',
                                value: shipping,
                            },
                            tax_total: {
                                currency_code: 'PHP',
                                value: "0",
                            }
                        }
                    },
                    items: [{
                        name: selectedItemDescription,
                        unit_amount: {
                            currency_code: 'PHP',
                            value: "270.00",
                        },
                        quantity: quantity
                    }]
                }]
            });
        },
        onApprove: function (data, actions) {
            return actions.order.capture().then(function (details) {
                //alert('Transaction completed by ' + details.payer.name.given_name + '!');
                dotNetHelper.invokeMethodAsync('OnApprove', data, details);
            });
        },
        onError: function (err) {
            console.log(err);
        },
    }).render('#paypal-button-container');
}
