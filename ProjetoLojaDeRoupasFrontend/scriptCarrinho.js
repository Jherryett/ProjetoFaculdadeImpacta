const API = "https://localhost:7179/api";

const tabelaBody = document.querySelector("#tabelaCarrinho tbody");

let carrinhoAtual = [];
let produtosCache = [];

window.onload = () => {
    carregarClientes();
    carregarProdutos();

    document.getElementById("clienteSelect")
        .addEventListener("change", carregarCarrinho);
};


async function carregarClientes() {
    const response = await fetch(`${API}/clientes`);
    const dados = await response.json();

    const clientes = Array.isArray(dados) ? dados : dados.$values;

    const select = document.getElementById("clienteSelect");
    select.innerHTML = "";

    clientes.forEach(c => {
        const opt = document.createElement("option");
        opt.value = c.id;
        opt.textContent = c.nomeCliente;
        select.appendChild(opt);
    });

    carregarCarrinho();
}


async function carregarProdutos() {
    const response = await fetch(`${API}/roupas`);
    const dados = await response.json();

    produtosCache = Array.isArray(dados) ? dados : dados.$values;

    const select = document.getElementById("produtoSelect");
    select.innerHTML = "";

    produtosCache.forEach(p => {
        const opt = document.createElement("option");
        opt.value = p.id;

        opt.textContent = `
${p.nomeRoupa ?? "-"} | Tam: ${p.tamanho ?? "-"} | Fab: ${p.nomeFabricante ?? "-"} | R$ ${p.valorPeca ?? 0}
`.trim();

        select.appendChild(opt);
    });
}


async function adicionarAoCarrinho() {

    const carrinho = {
        clienteId: parseInt(clienteSelect.value),
        roupaId: parseInt(produtoSelect.value),
        quantidade: parseInt(quantidade.value)
    };

    const response = await fetch(`${API}/carrinhos`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(carrinho)
    });

    if (!response.ok) {
        alert("Erro ao adicionar item");
        return;
    }

    carregarCarrinho();
}


async function carregarCarrinho() {

    const clienteId = clienteSelect.value;
    if (!clienteId) return;

    const response = await fetch(`${API}/carrinhos?clienteId=${clienteId}`);

    const dados = await response.json();
    carrinhoAtual = Array.isArray(dados) ? dados : dados.$values;

    preencherTabelaCarrinho(carrinhoAtual);
}


function preencherTabelaCarrinho(itens) {

    tabelaBody.innerHTML = "";

    let total = 0;

    itens.forEach(item => {

        const produto = produtosCache.find(p => p.id === item.roupaId);

        const descricaoProduto = produto
            ? `${produto.nomeRoupa ?? "-"} | Tam: ${produto.tamanho ?? "-"} | Fab: ${produto.nomeFabricante ?? "-"}`
            : `ID ${item.roupaId}`;

        const subtotal = item.quantidade * item.valorUnitario;
        total += subtotal;

        const tr = document.createElement("tr");

        tr.innerHTML = `
            <td>${descricaoProduto}</td>

            <td>
                <input type="number" value="${item.quantidade}" min="1"
                    onchange="atualizarQuantidade(${item.id}, this.value)">
            </td>

            <td>R$ ${item.valorUnitario.toFixed(2)}</td>
            <td>R$ ${subtotal.toFixed(2)}</td>

            <td>
                <button onclick="removerItem(${item.id})">Remover</button>
            </td>
        `;

        tabelaBody.appendChild(tr);
    });

    document.getElementById("totalCarrinho")
        .textContent = `Total: R$ ${total.toFixed(2)}`;
}


async function atualizarQuantidade(id, novaQtd) {

    const item = carrinhoAtual.find(x => x.id === id);
    if (!item) return;

    item.quantidade = parseInt(novaQtd);

    const response = await fetch(`${API}/carrinhos`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(item)
    });

    if (!response.ok) {
        alert("Erro ao atualizar");
        return;
    }

    carregarCarrinho();
}


async function removerItem(id) {

    const response = await fetch(`${API}/carrinhos/${id}`, {
        method: "DELETE"
    });

    if (!response.ok) {
        alert("Erro ao remover");
        return;
    }

    carregarCarrinho();
}