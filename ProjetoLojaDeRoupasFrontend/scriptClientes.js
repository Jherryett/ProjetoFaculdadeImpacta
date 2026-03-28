const form = document.getElementById("formCliente");
const btnListarTodos = document.getElementById("btnListarTodos");
const btnListarPorCodigo = document.getElementById("btnListarPorCodigo");
const btnAtualizar = document.getElementById("btnAtualizar");
const btnApagar = document.getElementById("btnApagar");
const tabelaBody = document.querySelector("#tabelaClientes tbody");




form.addEventListener("submit", async (event) => {
    event.preventDefault();

    const cliente = {
        nomeCliente: document.getElementById("nomeCliente").value,
        dataNascimento: document.getElementById("dataNascimento").value,
        cpd: document.getElementById("cpf").value,
        numeroTelefone: document.getElementById ("numeroTelefone").value,
        email: document.getElementById ("email").value,
        observacoes: document.getElementById("observacoes").value
    };

    try {
        const response = await fetch("https://localhost:7179/api/clientes", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(roupa)
        });

        if (!response.ok) {
            throw new Error("Erro ao cadastrar cliente");
        }

        alert("Cliente cadastrado com sucesso!");
        form.reset();

    } catch (error) {
        console.error(error);
        alert("Erro ao enviar informação para a API");
    }
});


btnListarTodos.addEventListener("click", listarTodos);

async function listarTodos() {
    const response = await fetch("https://localhost:7179/api/clientes");
    const roupas = await response.json();
    preencherTabela(roupas); }




btnAtualizar.addEventListener("click", atualizarCliente);

async function atualizarCliente() {
    const roupa = montarRoupa(true);

    await fetch("https://localhost:7179/api/clientes", {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(cliente)
    });

    alert("Cliente atualizado!");}



btnApagar.addEventListener("click", apagarCliente);

async function apagarCliente() {
    const id = document.getElementById("codigoRoupa").value;

    if (!id) {
        alert("Informe o código");
        return;
    }

    await fetch(`https://localhost:7179/api/clientes/${id}`, {
        method: "DELETE"
    });

    alert("Cliente apagado!");
    tabelaBody.innerHTML = "";
}


function montarCliente(incluirId) {

    const cliente = {
        nomeCliente: document.getElementById("nomeCliente").value,
        dataNascimento: document.getElementById("dataNascimento").value,
        cpd: document.getElementById("cpf").value,
        numeroTelefone: document.getElementById ("numeroTelefone").value,
        email: document.getElementById ("email").value,
        observacoes: document.getElementById("observacoes").value   
    };

    if (incluirId) {
        cliente.Id = parseInt(document.getElementById("codigoCliente").value);
    }

    return cliente;
}



function preencherTabela(clientes) {

    tabelaBody.innerHTML = "";

    clientes.forEach(r => {

        const tr = document.createElement("tr");

        tr.innerHTML = `
            <td>${r.id}</td>
            <td>${r.nomeCliente}</td>
            <td>${r.dataNascimento}</td>
            <td>${r.cpf}</td>
            <td>${r.numeroTelefone}</td>
            <td>${r.email}</td>
            <td>${r.observacoes}</td>
        `;

        
        tr.addEventListener("click", () => carregarNoFormulario(r));

        tabelaBody.appendChild(tr);
    });
}

function carregarNoFormulario(cliente) {

    document.getElementById("codigoCliente").value = cliente.id;
    document.getElementById("nomeCliente").value = cliente.nomeCliente ?? "";
    document.getElementById("dataNascimento").value = cliente.dataNascimento ?? "";
    document.getElementById("cpf").value = cliente.cpf ?? "";
    document.getElementById("numeroTelefone").value = cliente.numeroTelefone ?? "";
    document.getElementById("email").value = cliente.email ?? "";
    document.getElementById("observacoes").value = cliente.observacoes ?? "";
}


const inputBuscarCodigo = document.getElementById("buscarCodigo");


inputBuscarCodigo.addEventListener("change", buscarPorCodigo);

async function buscarPorCodigo() {

    const id = inputBuscarCodigo.value;

    if (!id) {
        tabelaBody.innerHTML = "";
        return;
    }

    try {
        const response = await fetch(`https://localhost:7179/api/clientes/${id}`);

        if (!response.ok) {
            alert("Cliente não encontrada");
            return;
        }

        const roupa = await response.json();

        preencherTabela([cliente]);
        carregarNoFormulario(cliente);

    } catch (erro) {
        alert("Erro ao buscar cliente");
        console.error(erro);
    }
}

