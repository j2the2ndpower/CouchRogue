using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TreasureOverlay : MonoBehaviour {
    public List<Collectible> TreasureItems;
    public List<Transform> ItemInstances;
    public List<CRPlayer> ItemOfferedTo;
    public CharacterHUD[] Portraits;

    public GameObject TreasureImage;
    public GameObject Cursor;
    public Text DescText;
    public Text NameText;
    public Transform objectParent;
    public CRPlayer Controller;
    public List<CRPlayer> offeredPlayers;
    public int HoverIndex = 0;
    public int PortraitIndex = 0;
    public bool SelectingPlayer = false;
    public bool WarningPlayer = false;
    public GameObject PlayerWarning;
    public CRPlayer HoveringPlayer;

    public Transform ItemParent;

    public void SetTreasures(List<Collectible> list, CRPlayer opener) {
        transform.SetSiblingIndex(0);
        Cursor.transform.SetParent(transform.parent);
        Cursor.transform.SetSiblingIndex(transform.parent.childCount);

        TreasureItems = list;
        Controller = opener;

        foreach(Collectible treasure in list) {
            Image img = Instantiate(TreasureImage, ItemParent).GetComponent<Image>();
            img.sprite = treasure.spriteTransform.GetComponent<SpriteRenderer>().sprite;

            ItemInstances.Add(img.transform);

            ItemOfferedTo.Add(null);
        }

        Cursor.GetComponentInChildren<Image>().color = Controller.unitColor;
        MoveCursorToItem(0);

        FindObjectOfType<Game>().OpenTreasureForPlayers(true, this);

        Portraits = FindObjectsOfType<CharacterHUD>();
    }

    private void Update() {
        if (SelectingPlayer) {
            Cursor.transform.position = Portraits[PortraitIndex].transform.position + new Vector3(20f, 250f, 0f);
        } else {
            Cursor.transform.position = ItemInstances[HoverIndex].position + new Vector3(100f, 350f, 0f);
        }
    }

    public void TreasureMove(InputAction.CallbackContext ctx, CRPlayer player) {
        Vector2 dir = ctx.ReadValue<Vector2>();
        if (player == Controller && ctx.started && !WarningPlayer) {
            if (dir.x < 0) {
                MoveLeft();
            } else if  (dir.x > 0) {
                MoveRight();
            }
        }
    }

    public void TreasureAffirm(InputAction.CallbackContext ctx, CRPlayer player) {
        if (player == Controller && ctx.started) {
            if (WarningPlayer) {
                CloseTreasureOverlay();
            } else if (!SelectingPlayer && ItemOfferedTo[HoverIndex] == null) {
                SelectingPlayer = true;
                PortraitIndex = Portraits.Length-1;
                MoveCursorToPortrait(PortraitIndex);
                Image im = ItemInstances[HoverIndex].GetComponent<Image>();
                im.color = new Color (im.color.r, im.color.g, im.color.b, 0.5f);
            } else if (SelectingPlayer) {
                if (HoveringPlayer == Controller) {
                    GiveItemToPlayer(HoverIndex, HoveringPlayer);
                } else if (HoveringPlayer != Controller && !offeredPlayers.Contains(HoveringPlayer)) {
                    HoveringPlayer.hud.ShowDescPanel(true, NameText.text, DescText.text, true);
                    offeredPlayers.Add(HoveringPlayer);
                    ItemOfferedTo[HoverIndex] = HoveringPlayer;
                    HoverIndex = 0;
                    MoveCursorToItem(0);
                    SelectingPlayer = false;
                }
            }
        } else if (offeredPlayers.Contains(player) && ctx.started) {
            foreach (CRPlayer p in ItemOfferedTo) {
                if (p == player) {
                    int index = ItemOfferedTo.IndexOf(p);
                    GiveItemToPlayer(index, player);
                    offeredPlayers.Remove(player);
                    player.hud.ShowDescPanel(false, ";", ";", false);
                    break;
                }
            }
        }
    }

    public void TreasureCancel(InputAction.CallbackContext ctx, CRPlayer player) {
        if (player == Controller && ctx.started) {
            if (WarningPlayer) {
                PlayerWarning.SetActive(false);
                Cursor.SetActive(true);
                DescText.transform.parent.gameObject.SetActive(true);
                WarningPlayer = false;
            } else if (SelectingPlayer) {
                SelectingPlayer = false;
                PortraitIndex = Portraits.Length-1;
                MoveCursorToItem(HoverIndex);
                Image im = ItemInstances[HoverIndex].GetComponent<Image>();
                im.color = new Color (im.color.r, im.color.g, im.color.b, 1f);
            } else {
                PlayerWarning.SetActive(true);
                WarningPlayer = true;
                Cursor.SetActive(false);
                DescText.transform.parent.gameObject.SetActive(false);
            }
        } else if (offeredPlayers.Contains(player) && ctx.started) {
            foreach (CRPlayer p in ItemOfferedTo) {
                if (p == player) {
                    int index = ItemOfferedTo.IndexOf(p);
                    ItemOfferedTo[index] = null;
                    Image im = ItemInstances[index].GetComponent<Image>();
                    im.color = new Color (im.color.r, im.color.g, im.color.b, 1f);
                    offeredPlayers.Remove(player);
                    player.hud.ShowDescPanel(false, "", "", false);
                    break;
                }
            }
        }
    }

    public void MoveCursorToItem(int index) {
        DescText.text = TreasureItems[index].ItemDescription;
        NameText.text = TreasureItems[index].ItemName;
    }

    public void MoveCursorToPortrait(int index) {
        HoveringPlayer = Portraits[index].player;
    }

    public void MoveLeft() {
        if (!SelectingPlayer && HoverIndex > 0) {
            HoverIndex--;
            MoveCursorToItem(HoverIndex);
        } else if (SelectingPlayer && PortraitIndex < Portraits.Length-1) {
            PortraitIndex++;
            MoveCursorToPortrait(PortraitIndex);
        }
    }

    public void MoveRight() {
        if (!SelectingPlayer && HoverIndex < ItemInstances.Count-1) {
            HoverIndex++;
            MoveCursorToItem(HoverIndex);
        } else if (SelectingPlayer && PortraitIndex > 0) {
            PortraitIndex--;
            MoveCursorToPortrait(PortraitIndex);
        }
    }

    public void CloseTreasureOverlay() {
        FindObjectOfType<Game>().OpenTreasureForPlayers(false, null);
        Destroy(Cursor);
        Destroy(gameObject);
    }

    public void GiveItemToPlayer (int index, CRPlayer player) {
        Collectible c = Instantiate(TreasureItems[index], objectParent);
            c.OnInteract(player);
            Destroy(ItemInstances[index].gameObject);
            ItemInstances.RemoveAt(index);
            TreasureItems.RemoveAt(index);
            ItemOfferedTo.RemoveAt(index);
            if (ItemInstances.Count != 0) {
                HoverIndex = 0;
                MoveCursorToItem(0);
                SelectingPlayer = false;
            } else {
                CloseTreasureOverlay();
        }
    }
}
