using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class inventorySys : MonoBehaviour
{
    WorldGeneration world;
    public GameObject selectedBlock;
    public Button[] BlockButtons;
    private void Start()
    {
        world = FindObjectOfType<WorldGeneration>();
        BlockButtons = GetComponentsInChildren<Button>();
        for (int i = 0; i < BlockButtons.Length; i++) // Asigna las funciones a cada boton, aunque ya me di cuenta que no los usé como botón, la puta madre
        {
            int index = i;
            GameObject cube = world.DefaultBlocks[index];
            BlockButtons[index].onClick.AddListener(delegate { AssingBlock(cube, BlockButtons[index]); });
        }
        AssingBlock(world.DefaultBlocks[0], BlockButtons[0]);
    }
    private void Update()
    {
        // Tranquilos, no soy YandereDev, así es la única forma de chequear input... creo...
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AssingBlock(world.DefaultBlocks[0], BlockButtons[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AssingBlock(world.DefaultBlocks[1], BlockButtons[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AssingBlock(world.DefaultBlocks[2], BlockButtons[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AssingBlock(world.DefaultBlocks[3], BlockButtons[3]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AssingBlock(world.DefaultBlocks[4], BlockButtons[4]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            AssingBlock(world.DefaultBlocks[5], BlockButtons[5]);
        }
    }
    private void AssingBlock(GameObject newBlock, Button thatButton) // Selecciona el bloque que querés poner :3
    {
        selectedBlock = newBlock;
        foreach (Button but in BlockButtons)
        {
            but.targetGraphic.color = new Color32(69, 69, 69, 255);
        }
        thatButton.targetGraphic.color = new Color32(69, 69, 69, 0);
    }
}
