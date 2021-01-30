using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoItem
{
    INDEFINIDO,
    CHOCALHO,
    CARTA,
    FILTRO
}

public enum IdItem
{
    INDEFINIDO,
    ID01,
    ID02,
    ID03
}

public class Itens : MonoBehaviour
{
    public TipoItem tipoItem;
    public IdItem idItem;
    public GameObject porta;
}
