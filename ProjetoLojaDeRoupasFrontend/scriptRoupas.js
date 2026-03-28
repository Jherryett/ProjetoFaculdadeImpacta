const form = document.getElementById("formRoupa");
const btnListarTodos = document.getElementById("btnListarTodos");
const btnListarPorCodigo = document.getElementById("btnListarPorCodigo");
const btnAtualizar = document.getElementById("btnAtualizar");
const btnApagar = document.getElementById("btnApagar");
const tabelaBody = document.querySelector("#tabelaRoupas tbody");




form.addEventListener("submit", async (event) => {
    event.preventDefault();

    const roupa = {
        nomeRoupa: document.getElementById("nomeRoupa").value,
        tamanho: document.getElementById("tamanhoRoupa").value,
        nomeFabricante: document.getElementById("nomeFabricante").value,
        valorPeca: parseFloat(document.getElementById("valorPeca").value),
        quantidadeEstoque: parseFloat(document.getElementById("quantidadeEstoque").value),
        observacoes: document.getElementById("observacoes").value
    };

    try {
        const response = await fetch("https://localhost:7179/api/roupas", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(roupa)
        });

        if (!response.ok) {
            throw new Error("Erro ao cadastrar roupa");
        }

        alert("Roupa cadastrada com sucesso!");
        form.reset();

    } catch (error) {
        console.error(error);
        alert("Erro ao enviar informação para a API");
    }
});


btnListarTodos.addEventListener("click", listarTodas);

async function listarTodas() {
    const response = await fetch("https://localhost:7179/api/roupas");
    const roupas = await response.json();
    preencherTabela(roupas); }




btnAtualizar.addEventListener("click", atualizarRoupa);

async function atualizarRoupa() {
    const roupa = montarRoupa(true);

    await fetch("https://localhost:7179/api/roupas", {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(roupa)
    });

    alert("Roupa atualizada!");}



btnApagar.addEventListener("click", apagarRoupa);

async function apagarRoupa() {
    const id = document.getElementById("codigoRoupa").value;

    if (!id) {
        alert("Informe o código");
        return;
    }

    await fetch(`https://localhost:7179/api/roupas/${id}`, {
        method: "DELETE"
    });

    alert("Roupa apagada!");
    tabelaBody.innerHTML = "";
}


function montarRoupa(incluirId) {

    const roupa = {
        nomeRoupa: document.getElementById("nomeRoupa").value,
        tamanho: document.getElementById("tamanhoRoupa").value,
        nomeFabricante: document.getElementById("nomeFabricante").value,
        valorPeca: document.getElementById("valorPeca").value
            ? parseFloat(document.getElementById("valorPeca").value)
            : null,
        quantidadeEstoque: document.getElementById("quantidadeEstoque").value
            ? parseFloat(document.getElementById("quantidadeEstoque").value)
            : null,
        observacoes: document.getElementById("observacoes").value
    };

    if (incluirId) {
        roupa.Id = parseInt(document.getElementById("codigoRoupa").value);
    }

    return roupa;
}



function preencherTabela(roupas) {

    tabelaBody.innerHTML = "";

    roupas.forEach(r => {

        const tr = document.createElement("tr");

        tr.innerHTML = `
            <td>${r.id}</td>
            <td>${r.nomeRoupa}</td>
            <td>${r.tamanho}</td>
            <td>${r.nomeFabricante}</td>
            <td>${formatarMoeda(r.valorPeca)}</td>
            <td>${r.quantidadeEstoque}</td>
            <td>${r.observacoes}</td>
        `;

        
        tr.addEventListener("click", () => carregarNoFormulario(r));

        tabelaBody.appendChild(tr);
    });
}

function carregarNoFormulario(roupa) {

    document.getElementById("codigoRoupa").value = roupa.id;
    document.getElementById("nomeRoupa").value = roupa.nomeRoupa ?? "";
    document.getElementById("tamanhoRoupa").value = roupa.tamanho ?? "";
    document.getElementById("nomeFabricante").value = roupa.nomeFabricante ?? "";
    document.getElementById("valorPeca").value = roupa.valorPeca ?? "";
    document.getElementById("quantidadeEstoque").value = roupa.quantidadeEstoque ?? "";
    document.getElementById("observacoes").value = roupa.observacoes ?? "";
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
        const response = await fetch(`https://localhost:7179/api/roupas/${id}`);

        if (!response.ok) {
            alert("Roupa não encontrada");
            return;
        }

        const roupa = await response.json();

        preencherTabela([roupa]);
        carregarNoFormulario(roupa);

    } catch (erro) {
        alert("Erro ao buscar roupa");
        console.error(erro);
    }
}

function formatarMoeda(valor) {
    if (valor === null || valor === undefined) return "";

    return new Intl.NumberFormat("pt-BR", {
        style: "currency",
        currency: "BRL"
    }).format(valor);
}
