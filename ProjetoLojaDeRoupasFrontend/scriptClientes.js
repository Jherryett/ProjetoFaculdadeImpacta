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
        cpf: document.getElementById("cpf").value,
        numeroTelefone: document.getElementById ("numeroTelefone").value,
        emailCliente: document.getElementById ("email").value,
        observacoes: document.getElementById("observacoes").value
    };

    try {
        const response = await fetch("https://localhost:7179/api/clientes", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(cliente)
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
    
    const texto = await response.text();
    console.log("RAW:", texto);

    const dados = JSON.parse(texto);
    console.log("JSON:", dados);

    const clientes = Array.isArray(dados) ? dados : dados.$values;

    console.log("FINAL:", clientes);

    preencherTabela(clientes);

}


btnAtualizar.addEventListener("click", atualizarCliente);

async function atualizarCliente() {
    const cliente = montarCliente(true);

    await fetch("https://localhost:7179/api/clientes", {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(cliente)
    });

    alert("Cliente atualizado!");}



btnApagar.addEventListener("click", apagarCliente);

async function apagarCliente() {
    const id = document.getElementById("codigoCliente").value;

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
        cpf: document.getElementById("cpf").value.replace(/\D/g, ""),
        numeroTelefone: document.getElementById ("numeroTelefone").value,
        emailCliente: document.getElementById ("email").value,
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
            <td>${formatarData(r.dataNascimento)}</td>
            <td>${formatarCPF(r.cpf)}</td>
            <td>${formatarTelefone(r.numeroTelefone)}</td>
            <td>${r.emailCliente}</td>
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
    document.getElementById("cpf").value = formatarCPF(cliente.cpf);
    document.getElementById("numeroTelefone").value = cliente.numeroTelefone ?? "";
    document.getElementById("email").value = cliente.emailCliente ?? "";
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

        const cliente = await response.json();

        preencherTabela([cliente]);
        carregarNoFormulario(cliente);

    } catch (erro) {
        alert("Erro ao buscar cliente");
        console.error(erro);
    }
}

function formatarData(data) {
    if (!data) return "";

    const d = new Date(data);
    return d.toLocaleDateString("pt-BR");
}

function formatarCPF(cpf) {
    if (!cpf) return "";

    cpf = cpf.toString().replace(/\D/g, "");

  
    cpf = cpf.padStart(11, "0");

    return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4");
}

function formatarTelefone(tel) {
    if (!tel) return "";

    tel = tel.replace(/\D/g, "");

    if (tel.length === 11) {
        return tel.replace(/(\d{2})(\d{5})(\d{4})/, 
            "($1) $2-$3");
    }

    if (tel.length === 10) {
        return tel.replace(/(\d{2})(\d{4})(\d{4})/, 
            "($1) $2-$3");
    }

    return tel;
}