document.addEventListener('DOMContentLoaded', () => {
    const apiKey = 'ZtVdh8XQ2U8pWI2gmZ7f796Vh8GllXoN7mr0djNf';
    const brothList = document.getElementById('broth-list');
    const proteinList = document.getElementById('protein-list');
    const placeOrderButton = document.getElementById('place-order');
    const orderConfirmation = document.getElementById('order-confirmation');
    let selectedBroth = null;
    let selectedProtein = null;

    
    fetch('https://api.tech.redventures.com.br/broths', {
        headers: {
            'x-api-key': apiKey
        }
    })
        .then(response => response.json())
        .then(data => {
            data.forEach(broth => {
                const img = document.createElement('img');
                img.src = broth.imageActive;
                img.alt = broth.name;
                img.addEventListener('click', () => {
                    selectedBroth = broth.id;
                    updateSelection(brothList, img);
                });
                brothList.appendChild(img);
            });
        });

    
    fetch('https://api.tech.redventures.com.br/proteins', {
        headers: {
            'x-api-key': apiKey
        }
    })
        .then(response => response.json())
        .then(data => {
            data.forEach(protein => {
                const img = document.createElement('img');
                img.src = protein.imageActive;
                img.alt = protein.name;
                img.addEventListener('click', () => {
                    selectedProtein = protein.id;
                    updateSelection(proteinList, img);
                });
                proteinList.appendChild(img);
            });
        });

    
    placeOrderButton.addEventListener('click', () => {
        if (selectedBroth && selectedProtein) {
            fetch('https://api.tech.redventures.com.br/orders', {
                method: 'POST',
                headers: {
                    'x-api-key': apiKey,
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    brothId: selectedBroth,
                    proteinId: selectedProtein
                })
            })
                .then(response => response.json())
                .then(data => {
                    orderConfirmation.innerHTML = `Pedido Realizado!<br>Order ID: ${data.id}<br>${data.description}<br><img src="${data.image}" alt="Ramen Image">`;
                })
                .catch(error => {
                    console.error('Erro ao realizar o pedido:', error);
                    orderConfirmation.innerHTML = 'Erro ao realizar o pedido. Tente novamente.';
                });
        } else {
            orderConfirmation.innerHTML = 'Por favor, escolha uma proteína e um caldo!.';
        }
    });

    function updateSelection(list, selectedImg) {
        Array.from(list.children).forEach(img => {
            img.style.border = img === selectedImg ? '2px solid #ff6347' : 'none';
        });
    }
});
