using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    // Vari�veis
    [SerializeField] private string nomeMapa;
    [SerializeField] private string nomeMenu;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOptions;
    [SerializeField] private GameObject painelAudio;
    [SerializeField] private GameObject painelControles;
    [SerializeField] private GameObject painelCreditos;
    public LevelLoader levelLoader;

    // M�todo para iniciar o jogo (carregar o mapa)
    public void Jogar()
    {
        // Inicia a transi��o e carrega o mapa ap�s o tempo de transi��o
        levelLoader.Transition(nomeMapa);
    }

    // usa a fun��o de toggle painel pq � melhor que o m�todo manual de escrever true e false, gasta menos linha
    public void AbrirOptions() => TogglePainel(painelMenuInicial, painelOptions);
    public void FecharOptions() => TogglePainel(painelOptions, painelMenuInicial);

    public void AbrirAudio() => TogglePainel(painelOptions, painelAudio);
    public void FecharAudio() => TogglePainel(painelAudio, painelOptions);

    public void AbrirControles() => TogglePainel(painelOptions, painelControles);
    public void FecharControles() => TogglePainel(painelControles, painelOptions);

    public void AbrirCreditos() => TogglePainel(painelMenuInicial, painelCreditos);
    public void FecharCreditos() => TogglePainel(painelCreditos, painelMenuInicial);

    // fun��o pra sair do jogo, mas s� funciona com ele completo
    public void Quit()
    {
        Debug.Log("Sair do Jogo"); //testar no editor
        Application.Quit(); 
    }

    // M�todo auxiliar para alternar entre dois pain�is
    private void TogglePainel(GameObject painelToHide, GameObject painelToShow)
    {
        painelToHide.SetActive(false);
        painelToShow.SetActive(true);
    }
}
